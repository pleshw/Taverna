using Taverna.Scripts.List;

namespace Taverna.Tests
{
    [TestClass]
    public class UnitTestRandomFunctions
    {
        private readonly RandomList<int> _randomService;

        public UnitTestRandomFunctions()
        {
            _randomService = [1 , 2 , 3 , 4 , 5];
        }

        [TestMethod]
        public void Test_Get_ReturnsElement()
        {
            int result = _randomService.Get();
            Assert.IsTrue( _randomService.Contains( result ) , "O elemento retornado não está na lista." );
        }

        [TestMethod]
        public void Test_GetExcept_ReturnsElementNotInExceptionList()
        {
            List<int> exceptionList = [2 , 3];
            int result = _randomService.GetExcept( exceptionList );
            Assert.IsFalse( exceptionList.Contains( result ) , "O elemento retornado está na lista de exceções." );
        }

        [TestMethod]
        public void Test_GetExceptLastResult_ReturnsDifferentElement()
        {
            _randomService.Get(); // Para definir o lastResult
            var result = _randomService.GetExceptLastResult();
            Assert.AreNotEqual( _randomService.GetExceptLastResult() , result , "O elemento retornado é igual ao lastResult anterior." );
        }

        [TestMethod]
        public void Test_FYatesShuffle_ShufflesList()
        {
            var originalList = new List<int>( _randomService );
            _randomService.FYatesShuffle();
            CollectionAssert.AreNotEqual( originalList , _randomService , "A lista não foi embaralhada corretamente." );
        }
    }
}