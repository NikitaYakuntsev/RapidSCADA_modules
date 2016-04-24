using System;
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EntityService.Repository;
using Entity;

namespace UnitTestProject
{
    [TestClass]
    public class DeviceRepositoryTest
    {

        private DeviceRepository devRep = DeviceRepository.GetInstance();
        private DataRepository dataRep = DataRepository.GetInstance();
        private CommandRepository comRep = CommandRepository.GetInstance();

        [TestMethod]
        public void TestDeviceSave()
        {
            Device device = new Device();
            int num = new Random().Next();
            device.Name = "TestDevice" + num;
            device.Working = true;
            devRep.Save(device);

            Device another = devRep.GetById(device.Id);
            Assert.AreEqual(device.Id, another.Id);
            Assert.AreEqual(device.Name, another.Name);
            Assert.AreEqual(device.Working, another.Working);
            Console.WriteLine(device.Id);
        }

        [TestMethod]
        public void TestDeviceUpdate()
        {
            Device device = new Device();
            int num = new Random().Next();
            device.Name = "TestDeviceUpdate" + num;
            device.Working = true;
            devRep.Save(device);

            string newName = "Hello world";
            bool newWorking = false;
            device.Name = newName;
            device.Working = newWorking;
            devRep.Update(device.Id, device);

            Device another = devRep.GetById(device.Id);
            Assert.AreEqual(newName, another.Name);
            Assert.AreEqual(newWorking, another.Working);
        }

        
    }
}
