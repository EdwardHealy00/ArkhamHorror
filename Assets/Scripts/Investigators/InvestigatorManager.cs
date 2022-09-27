using System.Collections.Generic;
using Cards;
using UnityEngine;

namespace Investigators
{
    public class InvestigatorManager : MonoBehaviour
    {
        public Dictionary<InvestigatorID, Investigator> Investigators;
        
        // Start is called before the first frame update
        void Start()
        {
            

        }
        
        public void CreateInvestigators()
        {
            Investigators = new Dictionary<InvestigatorID, Investigator>
            {
                //{InvestigatorID.Dilettante, CreateDilletante()},
                //{InvestigatorID.Waitress, CreateWaitress()},
                //{InvestigatorID.Entertainer, CreateEntertainer()},
                //{InvestigatorID.Mobster, CreateMobster()},
                //{InvestigatorID.Urchin, CreateUrchin()},
                //{InvestigatorID.Astronomer, CreateAstronomer()},
                //{InvestigatorID.Reporter, CreateReporter()},
                //{InvestigatorID.Mechanic, CreateMechanic()},
                //{InvestigatorID.Haunted, CreateHaunted()},
                //{InvestigatorID.Magician, CreateMagician()},
                {InvestigatorID.Secretary, CreateSecretary()},
                {InvestigatorID.RookieCop, CreateRookieCop()},
            };
        }

        private Investigator CreateRookieCop()
        {
            return new(InvestigatorID.RookieCop, new Health(7, 5), AssignSkills(2, 2, 3, 3, 3),
                AssetsGenerator.CreateRookieCopStartPoss(), 2, 2);
        }

        private Investigator CreateSecretary()
        {
            return new(InvestigatorID.Secretary, new Health(6, 6), AssignSkills(3, 3, 3, 2, 2),
                AssetsGenerator.CreateSecretaryStartPoss(), 2, 2);
        }

        private Investigator CreateMagician()
        {
            throw new System.NotImplementedException();
        }

        private Investigator CreateHaunted()
        {
            throw new System.NotImplementedException();
        }

        private Investigator CreateMechanic()
        {
            throw new System.NotImplementedException();
        }

        private Investigator CreateReporter()
        {
            throw new System.NotImplementedException();
        }

        private Investigator CreateAstronomer()
        {
            throw new System.NotImplementedException();
        }

        private Investigator CreateUrchin()
        {
            throw new System.NotImplementedException();
        }

        private Investigator CreateMobster()
        {
            throw new System.NotImplementedException();
        }

        private Investigator CreateEntertainer()
        {
            throw new System.NotImplementedException();
        }

        private Investigator CreateWaitress()
        {
            throw new System.NotImplementedException();
        }

        private Investigator CreateDilletante()
        {
            throw new System.NotImplementedException();
        }

        private static Dictionary<SkillID, Skill> AssignSkills(uint lore, uint influence, uint observation, uint strength, uint will)
        {
            return new Dictionary<SkillID, Skill>
            {
                {SkillID.Lore, new Skill(SkillID.Lore, lore)},
                {SkillID.Influence, new Skill(SkillID.Influence, influence)},
                {SkillID.Observation, new Skill(SkillID.Observation, observation)},
                {SkillID.Strength, new Skill(SkillID.Strength, strength)},
                {SkillID.Will, new Skill(SkillID.Will, will)},
            };
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
