using System.Collections;
using NUnit.Framework;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine;

namespace Tests
{
    public class CoinTest
    {
        [UnityTest]
        public IEnumerator CoinTestWithEnumeratorPasses()
        {
            SceneManager.LoadScene("Test");
            yield return null; // ждём кадр
            
            var test = new MonoBehaviourTest<CoinTestBehavior>();
            yield return test; // ждем выполнение теста

            var coin = GameObject.FindWithTag("Coin");
            Assert.IsTrue(coin == null, "Coin should have been destroyed");
        }

        [UnityTest]
        public IEnumerator IncreaseCoinTest()
        {
            SceneManager.LoadScene("Test");
            yield return null; // ждём кадр
            
            var test = new MonoBehaviourTest<CoinTestBehavior>();
            yield return test; // ждем выполнение теста
            
            // ожидаем такое сообщение, когда соберем первую монету - Score: 1
            LogAssert.Expect(LogType.Log, "Score: 1"); 
        }
    }
}
