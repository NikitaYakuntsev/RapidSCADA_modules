using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EntityService.Repository;
using Entity;

namespace UnitTestProject
{   
    [TestClass]
    public class DataRepositoryTest
    {
        private DeviceRepository devRep = DeviceRepository.GetInstance();
        private DataRepository dataRep = DataRepository.GetInstance();

        [TestMethod]
        public void TestDataSave()
        {
            Device device = new Device();
            device.Name = "TestDataSave" + new Random().Next();
            device.Working = true;
            devRep.Save(device);

            Data data = new Data();
            data.Name = "TestDataSave1";
            data.Timestamp = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0)).Seconds;
            data.Value = new Random().Next();
            data.Device = device;
            dataRep.Save(data);

            Data another = dataRep.GetById(data.Id);

            Assert.AreEqual(another.Device.Id, data.Device.Id);

            foreach (var cur in device.Data)
                Assert.AreEqual(another.Id, cur.Id);
        }

        [TestMethod]
        public void TestDataUpdate()
        {
            Device device = new Device();
            device.Name = "TestDataUpdate" + new Random().Next();
            device.Working = true;
            devRep.Save(device);

            Data data = new Data();
            data.Name = "TestDataUpdate";
            data.Timestamp = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0)).Seconds;
            data.Value = new Random().Next();
            data.Device = device;
            dataRep.Save(data);

            string newName = "newName";
            long newTs = 1234;
            double value = 12345;
            data.Name = newName;
            data.Timestamp = newTs;
            data.Value = value;
            dataRep.Update(data.Id, data);


            Data another = dataRep.GetById(data.Id);
            Assert.AreEqual(newName, another.Name);
            Assert.AreEqual( newTs, another.Timestamp);
            Assert.AreEqual(value, another.Value);
        }
    }
}
