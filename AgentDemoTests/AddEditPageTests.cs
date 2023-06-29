using Microsoft.VisualStudio.TestTools.UnitTesting;
using AgentDemo.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgentDemo.Models;

namespace AgentDemo.Pages.Tests
{
    [TestClass()]
    public class AddEditPageTests
    {
        public static AgentDemo2Entities db = new AgentDemo2Entities();
        Agent agent = new Agent()
        {
            Title = "Title1",
            AgentTypeID = 2,
            INN = "11111111",
            KPP = "11111111",
            Phone = "89998887766",
            Priority = 100

        };

        [TestMethod()]
        public void AddEditPageTest()
        {
            App.db.Agent.Add(agent);
            App.db.SaveChanges();
            var ag = App.db.Agent.Where(x=>x.Title == agent.Title && x.AgentTypeID ==  agent.AgentTypeID && x.INN == agent.INN).FirstOrDefault();
            Assert.IsTrue(ag != null);
            //App.db.Agent.Remove(agent);
            //App.db.SaveChanges();
        }

        [TestMethod()]
        public void EditAgentTest()
        {
            App.db.Agent.Add(agent);
            App.db.SaveChanges();
            var ag = App.db.Agent.Where(x => x.Title == agent.Title && x.AgentTypeID == agent.AgentTypeID && x.INN == agent.INN).FirstOrDefault();
            agent.Title = "Name1";
            App.db.SaveChanges();
            var ag2 = App.db.Agent.Where(x => x.Title == agent.Title && x.AgentTypeID == agent.AgentTypeID && x.INN == agent.INN).FirstOrDefault();
            Assert.IsTrue(ag2 != null);
            //App.db.Agent.Remove(agent);
            //App.db.SaveChanges();
        }
        [TestMethod()]
        public void RemoveAgentTest()
        {
            App.db.Agent.Add(agent);
            App.db.SaveChanges();
            var ag = App.db.Agent.Where(x => x.Title == agent.Title ).FirstOrDefault();
            App.db.Agent.Remove(agent);
            Assert.IsTrue(ag != null);
        }
    }
}