
using UnityEngine;


public enum Culture {
    European,
    Asian
}

public enum Country : int {
    US = 1,
    Canada = 2
}

public enum Language {
    English,
    Spanish
}

public enum Status {
    Paused,
    Ready,
    Simulating
}


[System.Serializable]
public class LanguageGroup {
    public Language language;
    
//    [Range(0f,1f)]
    public float population;

    public LanguageGroup(Language language, float population) {
        this.language = language;
        this.population = population;
    }


}

[System.Serializable]
public class MigrationRate {
    public Region dest;
    public float rate;

    public MigrationRate(Region dest, float rate) {
        this.dest = dest;
        this.rate = rate;
    }
}