﻿// Copyright 2017 Oleg Petrochenko
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using HwdgWrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HwdgApiTests
{
    [TestClass]
    public class RstPulseOnStartup
    {
        private static IWrapper hwdg;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Console.WriteLine($"Test class {context.TestName} begin");
            hwdg = AssemblyBase.Wrapper;
            hwdg.FactoryResetAndWaitForReady();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            hwdg.Stop();
            hwdg.RstPulseOnStartupDisable();
        }

        [TestMethod]
        public void CanDisableRstPulseWhenRstPulseEnabled()
        {
            hwdg.RstPulseOnStartupEnable();

            Assert.AreEqual(Response.RstPulseOnStartupDisableOk, hwdg.RstPulseOnStartupDisable());
            Assert.IsFalse(hwdg.GetStatus().State.HasFlag(WatchdogState.RstPulseEnabled));
            Assert.IsFalse(hwdg.GetStatus().State.HasFlag(WatchdogState.PwrPulseEnabled));
            Assert.IsFalse(hwdg.GetStatus().State.HasFlag(WatchdogState.IsRunning));
        }

        [TestMethod]
        public void CanDisableRstPulseWhenRstPulseDisabled()
        {
            Assert.AreEqual(Response.RstPulseOnStartupDisableOk, hwdg.RstPulseOnStartupDisable());
            Assert.IsFalse(hwdg.GetStatus().State.HasFlag(WatchdogState.RstPulseEnabled));
            Assert.IsFalse(hwdg.GetStatus().State.HasFlag(WatchdogState.PwrPulseEnabled));
            Assert.IsFalse(hwdg.GetStatus().State.HasFlag(WatchdogState.IsRunning));
        }

        [TestMethod]
        public void CanDisableRstPulseWhenMonitoring()
        {
            hwdg.Start();
            Assert.AreEqual(Response.RstPulseOnStartupDisableOk, hwdg.RstPulseOnStartupDisable());
            Assert.IsFalse(hwdg.GetStatus().State.HasFlag(WatchdogState.RstPulseEnabled));
            Assert.IsFalse(hwdg.GetStatus().State.HasFlag(WatchdogState.PwrPulseEnabled));
            Assert.IsTrue(hwdg.GetStatus().State.HasFlag(WatchdogState.IsRunning));
        }

        [TestMethod]
        public void CanDisableRstPulseWhenWaitingForReboot()
        {
            const Int32 timeout = 5000;

            hwdg.SetResponseTimeout(timeout);
            hwdg.Start();
            hwdg.WaitForFlag(WatchdogState.WaitingForReboot);

            Assert.AreEqual(Response.RstPulseOnStartupDisableOk, hwdg.RstPulseOnStartupDisable());
            Assert.IsFalse(hwdg.GetStatus().State.HasFlag(WatchdogState.RstPulseEnabled));
            Assert.IsFalse(hwdg.GetStatus().State.HasFlag(WatchdogState.PwrPulseEnabled));
            Assert.IsTrue(hwdg.GetStatus().State.HasFlag(WatchdogState.IsRunning));
            Assert.IsTrue(hwdg.GetStatus().State.HasFlag(WatchdogState.WaitingForReboot));
        }

        [TestMethod]
        public void CanEnableRstPulseWhenRstPulseEnabled()
        {
            hwdg.RstPulseOnStartupEnable();

            Assert.AreEqual(Response.RstPulseOnStartupEnableOk, hwdg.RstPulseOnStartupEnable());
            Assert.IsTrue(hwdg.GetStatus().State.HasFlag(WatchdogState.RstPulseEnabled));
            Assert.IsFalse(hwdg.GetStatus().State.HasFlag(WatchdogState.PwrPulseEnabled));
            Assert.IsFalse(hwdg.GetStatus().State.HasFlag(WatchdogState.IsRunning));
        }

        [TestMethod]
        public void CanEnableRstPulseWhenRstPulseDisabled()
        {
            Assert.AreEqual(Response.RstPulseOnStartupEnableOk, hwdg.RstPulseOnStartupEnable());
            Assert.IsTrue(hwdg.GetStatus().State.HasFlag(WatchdogState.RstPulseEnabled));
            Assert.IsFalse(hwdg.GetStatus().State.HasFlag(WatchdogState.PwrPulseEnabled));
            Assert.IsFalse(hwdg.GetStatus().State.HasFlag(WatchdogState.IsRunning));
        }

        [TestMethod]
        public void CanEnableRstPulseWhenMonitoring()
        {
            hwdg.Start();
            Assert.AreEqual(Response.RstPulseOnStartupEnableOk, hwdg.RstPulseOnStartupEnable());
            Assert.IsTrue(hwdg.GetStatus().State.HasFlag(WatchdogState.RstPulseEnabled));
            Assert.IsFalse(hwdg.GetStatus().State.HasFlag(WatchdogState.PwrPulseEnabled));
            Assert.IsTrue(hwdg.GetStatus().State.HasFlag(WatchdogState.IsRunning));
        }

        [TestMethod]
        public void CanEnableRstPulseWhenWaitingForReboot()
        {
            const Int32 timeout = 5000;

            hwdg.SetResponseTimeout(timeout);
            hwdg.Start();
            hwdg.WaitForFlag(WatchdogState.WaitingForReboot);

            Assert.AreEqual(Response.RstPulseOnStartupEnableOk, hwdg.RstPulseOnStartupEnable());
            Assert.IsTrue(hwdg.GetStatus().State.HasFlag(WatchdogState.RstPulseEnabled));
            Assert.IsFalse(hwdg.GetStatus().State.HasFlag(WatchdogState.PwrPulseEnabled));
            Assert.IsTrue(hwdg.GetStatus().State.HasFlag(WatchdogState.IsRunning));
            Assert.IsTrue(hwdg.GetStatus().State.HasFlag(WatchdogState.WaitingForReboot));
        }
    }
}