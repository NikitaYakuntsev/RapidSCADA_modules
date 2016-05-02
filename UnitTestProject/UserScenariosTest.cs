using System;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Entity;
using EntityService.Repository;
using UnitTestProject.Generators;
using System.Collections.Generic;
using EntityService;
using EntityService.Service;
using Entity.Models;


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
        private static SystemParameterRepository spr = SystemParameterRepository.GetInstance();

        private Device device = null;
        private List<Command> command = null;
        private Token token = null;


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
            newRecord.Timestamp = Utils.GetUnixTime();
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

        [TestMethod]
        public void TestInitialize()
        {
            SystemParameter reg = spr.GetByKey(Common.Dictionary.SystemProperties.SP_REGISTERED);
            if (reg != null)
                spr.Remove(reg.Id);
            Initialization.Initialize();
            Assert.IsFalse(SystemPropertyService.GetInstance().IsRegistered());

            Initialization.Register();

            Assert.IsTrue(SystemPropertyService.GetInstance().IsRegistered());
            Assert.AreEqual(SystemPropertyService.GetInstance().GetValue(Common.Dictionary.SystemProperties.SP_SCADA_ID), "123");
        }
    }
}
