using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Region : MonoBehaviour {

	public static Dictionary<Language, Color> ColorDict = new Dictionary<Language, Color> {
		{Language.English, Color.red },
		{Language.Spanish, Color.blue},
		{Language.MandarinChinese, Color.yellow},
		{Language.Hindustani, Color.magenta},
		{Language.Arabic, Color.cyan}
	};

	public LanguageGroup[] defaultLP() {
		return new LanguageGroup[] {
			new LanguageGroup(Language.English, Random.Range(0f,10f)), 
			new LanguageGroup(Language.Spanish, Random.Range(0f,10f)),
			new LanguageGroup(Language.MandarinChinese, Random.Range(0f,10f)),
			new LanguageGroup(Language.Hindustani, Random.Range(0f,10f)),
			new LanguageGroup(Language.Arabic, Random.Range(0f,10f)),
		};
	}


	private Dictionary<Language, float> DefaultLanguageDict() {
		return new Dictionary<Language, float> () {
			{ Language.English, 0f },
			{ Language.Spanish, 0f },
			{ Language.MandarinChinese, 0f },
			{ Language.Arabic, 0f },
			{ Language.Hindustani, 0f }
		};
	}

	public static float birthRate = 0.02f;
	public static float deathRate = 0.01f;
	public static float retentionRate = 0.5f;

	private float localRetentionRate; 
	public float totalPopulation = 1f;
	public float GDP;
	public Language officialLanguage;
    public LanguageGroup[] primary;
    public LanguageGroup[] secondary;
    public MigrationRate[] migrationRates;
	
    public bool initialized = false;

    private SpriteRenderer render;

    private void Start() {
        render = GetComponent<SpriteRenderer>();
		localRetentionRate = (officialLanguage == Language.None) ? 1f : retentionRate;
		officialLanguage = (Language)Random.Range (1, 5);
    }

    public void GetReady() {
        Region[] regions = FindObjectsOfType<Region>();
        migrationRates = new MigrationRate[regions.Length - 1];
        int index = 0;
        for (int i = 0; i != regions.Length; i++) {
            if (regions[i] != this) {
				migrationRates[index] = new MigrationRate(regions[i], Random.Range(0f,0.1f));
                index++;
            }
                
        }

        primary = defaultLP();
        secondary = defaultLP();
		UpdateColor ();
        initialized = true;
    }

	public void UpdateColor() {
        LanguageGroup mostSpoken = primary[0];
        float population = 0f;
        foreach (LanguageGroup lp in primary) {
            population += lp.population;
            if (lp.population > mostSpoken.population) {
                mostSpoken = lp;
            }
        }
        Color color = ColorDict[mostSpoken.language];
        color.a = (mostSpoken.population / population);
        render.color = color;
		totalPopulation = population;

		Migrate ();
		LocalLearners ();
		PopulationGrowth ();
    }


	public void GetImmigrants(Language l, float p) {

		
        for (int i=0; i!=primary.Length; i++) {
        
			if (primary[i].language == l) {
                primary[i].population += p;
            }

			if (secondary[i].language == officialLanguage && l != officialLanguage) {
				secondary [i].population += p;
			}

        }

    }
   

	private void Migrate() {
        foreach (MigrationRate mrPair in migrationRates) {
            foreach (LanguageGroup lg in primary) {
                float migrants = lg.population * mrPair.rate;
                lg.population -= migrants;
                mrPair.dest.GetImmigrants(lg.language, migrants);
            }
        }
    }


	private void LocalLearners() {

		Dictionary<Language, float> dict = DefaultLanguageDict ();

		foreach (LanguageGroup lg in primary) {

			foreach (MigrationRate mrPair in migrationRates) {
				// use migration rate as approximation of business attraction
				float growth = mrPair.rate * Mathf.Sqrt((lg.population / totalPopulation));
				dict [lg.language] += growth;
			}
		}
		foreach (LanguageGroup lg in secondary) {
			lg.population += dict [lg.language];
		}
	}

	private void PopulationGrowth() {

		for (int i = 0; i != primary.Length; i++) {

			if (primary [i].language == officialLanguage) {
				primary [i].population += totalPopulation * birthRate
				- primary [i].population * deathRate;
			} else {
				primary [i].population -= primary [i].population * deathRate;
			}

			// TODO: Assuming secondary language doesn't pass to children
			secondary[i].population *= (1f - deathRate);
		}

//		foreach (LanguageGroup lg in primary) {
//			if (lg.language == officialLanguage) {
//				lg.population += totalPopulation * birthRate;
//				lg.population -= lg.population * deathRate;
//			} else {
//				// lg.population += lg.population * (birthRate * retentionRate - deathRate);
//				lg.population -= lg.population * deathRate;
//			}
//		}
//
//		foreach (LanguageGroup lg in secondary) {
//					
//			if (lg.language == officialLanguage) {
//				
//			}
//		}
	}

	private float Sigmoid(float x) {
		// Range (0,1)
		return 1f / (1f + Mathf.Exp (-x));
	}
}