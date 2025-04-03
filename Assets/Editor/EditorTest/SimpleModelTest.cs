using Components;
using NUnit.Framework; // библиотека тестов

// смысл теста в том, чтобы протестировать логику работы наших классов и методов
namespace Editor.EditorTest
{
    public class SimpleModelTest
    {
        private SimpleModel _simpleModel;

        // перед каждым тестом мы должны создавать экземпляр класса,
        // [SetUp] - поможет нам избежать этого, раз создали и всё
        [SetUp]
        public void Initialize()
        {
            _simpleModel = new SimpleModel(2, 3);
        }
        
        [Test]
        public void TestIncreaseValue()
        {
            _simpleModel.IncreaseValue();
            Assert.AreEqual(2, _simpleModel.Value); // по итогу ожидаем 2, и получаем 2
        }

        [Test]
        public void TestDecreaseValue()
        {
            _simpleModel.DecreaseValue();
            Assert.AreEqual(-3, _simpleModel.Value); // по итогу ожидаем -3, и получаем -3
        }
    }
}