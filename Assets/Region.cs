using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Region : MonoBehaviour {

    public static Dictionary<Language, Color> ColorDict = new Dictionary<Language, Color> {
        {Language.English, Color.red },
        {Language.Spanish, Color.blue}
    };

    public LanguageGroup[] defaultLP() {
        return new LanguageGroup[] {
            new LanguageGroup(Language.English, 1f), new LanguageGroup(Language.Spanish, 0f)
        };
    }

    public LanguageGroup[] first;
    public LanguageGroup[] secondary;
    public MigrationRate[] mr;
    public bool initialized = false;

    private SpriteRenderer render;

    private void Start() {
        render = GetComponent<SpriteRenderer>();
    }

    public void GetReady() {
        Region[] regions = FindObjectsOfType<Region>();
        mr = new MigrationRate[regions.Length - 1];
        int index = 0;
        for (int i = 0; i != regions.Length; i++) {
            if (regions[i] != this) {
                mr[index] = new MigrationRate(regions[i], 0f);
                index++;
            }
                
        }

        first = defaultLP();
        secondary = defaultLP();

        initialized = true;
    }

    private void UpdateColor() {
        LanguageGroup mostSpoken = first[0];
        float population = 0f;
        foreach (LanguageGroup lp in first) {
            population += lp.population;
            if (lp.population > mostSpoken.population) {
                mostSpoken = lp;
            }
        }
        Color color = ColorDict[mostSpoken.language];
        color.a = (mostSpoken.population / population);
        render.color = color;
    }


    public void GetFirst(Language l, float p) {
        for (int i=0; i!=first.Length; i++) {
            if (first[i].language == l) {
                first[i].population += p;
                return;
            }
        }
    }
    void Update() {
        if (initialized)
            UpdateColor();
    }

    public void Migrate() {
        foreach (MigrationRate mrPair in mr) {

            foreach (LanguageGroup lg in first) {
                float migrants = lg.population * mrPair.rate;
                lg.population -= migrants;
                mrPair.dest.GetFirst(lg.language, migrants);
            }
        }
    }
}
