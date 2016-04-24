using System;
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EntityService.Repository;
using Entity;
using UnitTestProject.Generators;

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
            Device device = EntityGenerator.GetDevice();
            devRep.Save(ref device);

            Device another = devRep.GetById(device.Id);
            Assert.AreEqual(device.Id, another.Id);
            Assert.AreEqual(device.Name, another.Name);
            Assert.AreEqual(device.Working, another.Working);
            Console.WriteLine(device.Id);
        }

        [TestMethod]
        public void TestDeviceUpdate()
        {
            Device device = EntityGenerator.GetDevice();         
            devRep.Save(ref device);

            string newName = "Hello world";
            bool newWorking = false;
            device.Name = newName;
            device.Working = newWorking;
            devRep.Update(device.Id, device);

            Device another = devRep.GetById(device.Id);
            Assert.AreEqual(newName, another.Name);
            Assert.AreEqual(newWorking, another.Working);
        }

        [TestMethod]
        public void TestRemoveDevice()
        {
            Device device = EntityGenerator.GetDevice();
            devRep.Save(ref device);

            devRep.Remove(device.Id);

            object actual = devRep.GetById(device.Id);
            Assert.AreEqual(null, actual);
        }

        [TestMethod]
        public void TestDevicePersistentState()
        {
            Device device = EntityGenerator.GetDevice();
            devRep.Save(ref device);

            string newName = "Hello World";
            device.Name = newName;
            //devRep.Update(device.Id, device);

            Device another = devRep.GetById(device.Id);
            Assert.AreEqual(newName, another.Name);
        }

        
    }
}
