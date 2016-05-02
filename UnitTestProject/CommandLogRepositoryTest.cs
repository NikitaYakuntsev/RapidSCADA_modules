using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EntityService.Repository;
using Entity;
using UnitTestProject.Generators;

namespace UnitTestProject
{
    [TestClass]
    public class CommandLogRepositoryTest
    {
        private CommandLogRepository clRep = CommandLogRepository.GetInstance();
        
        [TestMethod]        
        public void TestCommandLogSave()
        {
            CommandLog c = EntityGenerator.GetCommandLog();
            clRep.Save(c);

            CommandLog act = clRep.GetById(c.Id);
            Assert.ReferenceEquals(act, c);
        }

        [TestMethod]
        public void TestCommandLogUpdate()
        {
            CommandLog c = ScenarioGenerator.GetFilledCommandLog();
            string name = "1234";
            bool sent = !c.Sent;
            long time = 1234;
            c.Name = name;
            c.Sent = sent;
            c.Timestamp = time;
            clRep.Update(c.Id, c);

            CommandLog act = clRep.GetById(c.Id);
            Assert.ReferenceEquals(c, act);

        }
    }
}
