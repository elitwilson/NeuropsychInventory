using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NeuropsychInventory.Models;

namespace NeuropsychInventory.ViewModels
{
    public class TestVM {
        public IList<Test> Tests { get; set; }
        public IList<Product> ProductsByTest { get; set; }

        public class Test {
            public string Abbreviation { get; set; }
            public int Id { get; set; }
        }

        public TestVM() {
            Tests = new List<Test>();
            ProductsByTest = new List<Product>();
        }

    }

    public class CreateProductVM {
        public int TestId { get; set; }
        public string TestName { get; set; }
        public Product Product { get; set; }
    }
}