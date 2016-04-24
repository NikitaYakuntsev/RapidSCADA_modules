using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EntityService.Repository;
using Entity;

namespace UnitTestProject
{
    [TestClass]
    public class CommandRepositoryTest
    {
        private DeviceRepository devRep = DeviceRepository.GetInstance();
        private CommandRepository comRep = CommandRepository.GetInstance();
        [TestMethod]
        public void TestCommandSave()
        {
            Device device = new Device();
            device.Name = "TestCommSave" + new Random().Next();
            device.Working = true;
            devRep.Save(device);

            Command com = new Command();
            com.Device = device;
            com.Name = "Turn device ON";
            com.Text = "turn_on";
            comRep.Save(com);

            Command another = comRep.GetById(com.Id);

            Assert.AreEqual(com.Id, another.Id);

            foreach (var cur in device.Command)
                Assert.AreEqual(com.Id, cur.Id);
        }


    }
}
