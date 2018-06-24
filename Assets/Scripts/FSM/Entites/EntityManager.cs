using System.Collections.Generic;

namespace FSMTest
{
    class EntityManager
    {
        private Dictionary<int, BaseEntity> EntityMap = new Dictionary<int, BaseEntity>();
        public static EntityManager Instance { get; }
        static EntityManager()
        {
            Instance = new EntityManager();
        }
        public void RegisterEntity(BaseEntity entity)
        {
            EntityMap.Add(entity.ID, entity);
        }

        public BaseEntity GetEntityFromID(int ID)
        {
            BaseEntity baseEntity = null;
            foreach(KeyValuePair<int, BaseEntity> item in EntityMap)
            {
                if (item.Key.Equals(ID))
                {
                    baseEntity = item.Value;
                }                   
            }
            return baseEntity;
        }
        
        public void RemoveEntity(BaseEntity entity)
        {
            EntityMap.Remove(entity.ID);
        }
    }
}
