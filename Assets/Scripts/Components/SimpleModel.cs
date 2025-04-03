namespace Components
{
    public class SimpleModel
    {
        public int Value { get; private set; }
        
        private int _increaseValue;
        private int _decreaseValue;
        
        public SimpleModel(int increaseValue, int decreaseValue)
        {
            _increaseValue = increaseValue;
            _decreaseValue = decreaseValue;
        }

        public void IncreaseValue()
        {
            Value += _increaseValue;
        }

        public void DecreaseValue()
        {
            Value -= _decreaseValue;
        }
    }
}