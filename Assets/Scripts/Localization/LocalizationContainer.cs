using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Localization/Container")]
public class LocalizationContainer : ScriptableObject
{
    [System.Serializable]
    public struct Word
    {
        public string key;
        public string val;
    }

    [System.Serializable]
    public struct WordGroup
    {
        public string groupName;
        public Word[] words;
    }

    public string languageName;
    public WordGroup[] wordGroups;

    public string GetWord(string key)
    {
        foreach (WordGroup wordGroup in wordGroups)
        {
            foreach (Word word in wordGroup.words)
            {
                if (word.key == key)
                {
                    return word.val;
                }
            }
        }

        return key;
    }
}
