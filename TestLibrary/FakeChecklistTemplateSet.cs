﻿using ChecklistManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLibrary;

namespace TestLibrary
{
    public class FakeChecklistTemplateSet : FakeDbSet<ChecklistTemplate>
    {
        public override ChecklistTemplate Find(params object[] keyValues)
        {
            return this.SingleOrDefault(d => d.Id == (int)keyValues.Single());
        }
    }
}
