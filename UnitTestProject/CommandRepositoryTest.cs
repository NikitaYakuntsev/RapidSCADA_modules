using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EntityService.Repository;
using Entity;
using UnitTestProject.Generators;

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
            Device device = EntityGenerator.GetDevice();
            devRep.Save(ref device);

            Command com = EntityGenerator.GetCommand();
            com.Device = device;
            comRep.Save(ref com);

            Command another = comRep.GetById(com.Id);

            Assert.AreEqual(com.Id, another.Id);

            foreach (var cur in device.Command)
                Assert.AreEqual(com.Id, cur.Id);
        }

        [TestMethod]
        public void TestCommandUpdate()
        {
            Device device = EntityGenerator.GetDevice();
            devRep.Save(ref device);

            Command com = EntityGenerator.GetCommand();
            com.Device = device;
            comRep.Save(ref com);

            string newName = "turnOn";
            string text = "turn_on";
            com.Name = newName;
            com.Text = text;
            comRep.Update(com.Id, com);

            Command act = comRep.GetById(com.Id);
            Assert.AreEqual(newName, act.Name);
            Assert.AreEqual(text, act.Text);
        }
    }
}
