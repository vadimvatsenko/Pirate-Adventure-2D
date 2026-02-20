using UnityEngine;

// синглтон для доступа ко всем предметам
namespace Creatures.CreaturesStateMachine.Player.Model.Definision
{
    [CreateAssetMenu(menuName = "Defs/DefsFacade", fileName = "DefsFacade")]

    public class DefFacade : ScriptableObject
    {
        [SerializeField] private InventoryItemDef items;
        public InventoryItemDef Items => items;
        
        private static DefFacade _instance;
        
        // свойство по которому будем забирать элементы с инвентаря
        public static DefFacade Instance 
            => _instance == null ? LoadDefs() : _instance;

        private static DefFacade LoadDefs()
        {
            // загрузится ScriptableObject из ассетов
            // предварительно нужно его создать в иерархии
            return _instance = Resources.Load<DefFacade>("DefsFacade/DefsFacade");
        }
    }
}