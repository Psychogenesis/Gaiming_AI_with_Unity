using System.ComponentModel.DataAnnotations;

namespace FSMTest
{
    public enum EntityNameType
    {
        [Display(Name = "UNKNOWN!")]
        UNKNOWN,

        [Display(Name = "Miner Bob")]
        MINER,

        [Display(Name = "Elsa")]
        WIFE
    }

    public class EntityType
    {
        public static string GetEntityName(int name)
        {
            switch((EntityNameType) name)
            {
                case EntityNameType.UNKNOWN:
                    return "UNKNOWN!";
                case EntityNameType.MINER:
                    return "Miner Bob";
                case EntityNameType.WIFE:
                    return "Elsa";
                default:
                    return "ERROR!ERROR!ERROR!";
            }
        }
    }
}
