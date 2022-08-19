using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Investigators.InvestigatorUtils;

public class Investigator : MonoBehaviour
{

    public string Name;
    public Condition Condition;
    public Dictionary<SkillID, Skill> Skills;
    public Dictionary<CardID, Card> Assets;
    public int focusLimit;
    public int focusAmount => Skills.Sum(skill => skill.Value.TimesFocused);
    public int focusLimitPerSkill = 1;
    public int dollars;
    public int remnants;

    public Investigator(Condition condition, Dictionary<SkillID, Skill> Skills, Dictionary<CardID, Card> startingPossessions, int focusLimit, int dollars)
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FocusSkill(SkillID skillId)
    {
        if (Skills[skillId].TimesFocused < focusLimitPerSkill)
        {
            Skills[skillId].FocusSkill();
        }
    }
    
    void UnfocusSkill(SkillID skillId)
    {
        if (Skills[skillId].TimesFocused > 0)
        {
            Skills[skillId].UnfocusSkill();
        }
        else
        {
            Debug.LogError("Tried to unfocus unfocused skill");
        }
    }
}

public class Skill
{
    public int TimesFocused;
    public int Value;
    public SkillID SkillID;

    public Skill(SkillID skillId, int value)
    {
        TimesFocused = 0;
        Value = value;
        SkillID = skillId;
    }

    public void FocusSkill()
    {
        TimesFocused++;
        Value++;
    }
    
    public void UnfocusSkill()
    {
        TimesFocused--;
        Value--;
    }
}

public class Condition
{
    public int Health;
    public int Sanity;
}

public enum CardID
{
    
}

public class Card
{
    
}
