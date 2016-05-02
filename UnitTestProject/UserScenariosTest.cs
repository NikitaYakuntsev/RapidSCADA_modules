using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Entity;
using EntityService.Repository;
using UnitTestProject.Generators;
using System.Collections.Generic;


namespace UnitTestProject
{
    [TestClass]
    public class UserScenariosTest
    {
        private static DeviceRepository devRep = DeviceRepository.GetInstance();
        private static DataRepository dataRep = DataRepository.GetInstance();
        private static CommandRepository commRep = CommandRepository.GetInstance();
        private static CommandLogRepository commLogRep = CommandLogRepository.GetInstance();
        private static TokenRepository tokRep = TokenRepository.GetInstance();

        private Device device = null;
        private List<Command> command = null;
        private Token token = null;

        public long UnixTimeNow()
        {
            var timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            return (long)timeSpan.TotalSeconds;
        }

        [TestInitialize]
        public void Prepare()
        {
            device = ScenarioGenerator.GetFilledDevice();
            Console.WriteLine("Device ID: " + device.Id);
            command = new List<Command>();
            for (int i = 0; i < 10; i++)
            {
                Command temp = ScenarioGenerator.GetFilledCommand(device);
                command.Add(temp);
                Console.WriteLine("Command ID: " + temp.Id);                
            }
            token = ScenarioGenerator.GetFilledToken();
           
        }

        [TestMethod]
        public void TestCommandSend()
        {
            int id = command[new Random().Next(0, command.Count)].Id;

            Command recieved = commRep.GetById(id);
            CommandLog newRecord = new CommandLog();
            newRecord.Command = recieved;
            newRecord.Name = recieved.Name;
            newRecord.Sent = false;
            newRecord.Timestamp = UnixTimeNow();
            newRecord.Token = token;
            commLogRep.Save(newRecord);
            Console.WriteLine("CommLog ID: " + newRecord.Id);


            List<CommandLog> logs = new List<CommandLog>(commLogRep.GetByDevice(device.Id));
            bool found = false;
            foreach (var log in logs)
                if (log.Id == newRecord.Id)
                    found = true;
            Assert.IsTrue(found);
        }
    }
}
