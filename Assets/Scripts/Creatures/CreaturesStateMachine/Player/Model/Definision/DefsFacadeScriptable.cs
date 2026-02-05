using UnityEngine;

// 1 По сути первый скрипт ScriptableObject он же синглтон, хранит информацию о
// 
namespace Creatures.CreaturesStateMachine.Player.Model.Definision
{
    [CreateAssetMenu(menuName = "Defs/DefsFacade", fileName = "DefsFacade")]
    public class DefsFacadeScriptable : ScriptableObject
    {
        [SerializeField] private InventoryItemDefScriptable items;
        public InventoryItemDefScriptable Items => items;
        
        private static DefsFacadeScriptable _instance;
        public static DefsFacadeScriptable Instance => _instance == null ? LoadDefs() : _instance;
        private static DefsFacadeScriptable LoadDefs()
        {
            return _instance = Resources.Load<DefsFacadeScriptable>("DefsFacade");
        }
    }
}