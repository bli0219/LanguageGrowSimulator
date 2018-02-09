using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Culture {
    European,
    Asian
}

public enum Country: int {
    US = 1,
    Canada = 2
}

public enum Language {
    English,
    Spanish
}

[System.Serializable]
public class Region : MonoBehaviour {

	/*
    public float locationX;
    public float locationY;
    public float capacity;
    public float influence;
    public float immiBarrier;
    public Culture culture;
    public Country country;
    public Language official;
	*/
	[System.Serializable] 
	public class LanguagePopulation
	{
		public Language language;
		public float population;
	}

	[System.Serializable] 
	public class MigrationRate
	{
		public Region dest;
		public float rate;

		public MigrationRate(Region dest, float rate) {
			this.dest = dest;
			this.rate = rate;
		}
	}

	public LanguagePopulation[] first;
	public LanguagePopulation[] secondary;
	public MigrationRate[] mr; 
	public bool paused = true;

    void Start () {



//		foreach (Region r in FindObjectsOfType<Region> ()) {
//			if (r != this) {
//				migrateRates[
//			}
//		}
	}

	public void Unpause() {
		Region[] regions = FindObjectsOfType<Region> ();
		mr = new MigrationRate[regions.Length-1];
		for (int i = 0; i!=regions.Length-1; i++) {
			mr [i] = new MigrationRate (regions [i], 0f);
		}

		first = new LanguagePopulation[2];
		secondary = new LanguagePopulation[2];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Migrate() {
		if (!paused) {
//			
			foreach (MigrationRate mrPair in mr) {

//				first
//				migrateRate.dest   migrateRate.rate * 
			
//				float migrateRate = migratePair.Value; // percent migrating to the dest
//				 
//				foreach (KeyValuePair<Language,float> speakerPair in first ) {
//
//					Language language = speakerPair.Key;
//					float migrants = speakerPair.Value * migrateRate; // migrants with this language
//
//					first[language] -= migrants; // migrants leave
//					dest.first[language] += migrants; // migrants arrive
//				}
//
//				foreach (KeyValuePair<Language,float> speakerPair in secondary) {
//
//					Language language = speakerPair.Key;
//					float migrants = speakerPair.Value * migrateRate; // migrants with this language
//
//					first[language] -= migrants; // migrants leave
//					dest.first[language] += migrants; // migrants arrive
//				}
//
			}
		}
	}


}
