using DotnetGraph.Algorithms.DfSearch;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetGraphTest.Algorithms.DfSearch
{
    [TestClass]
    public class DfSearchTests
    {


        [TestMethod]
        public void CanReproduceExample()
        {
            var nodes = GetCormenExample();
            DepthFirstSearch searchAlgorithm = new DepthFirstSearch();
            searchAlgorithm.Run(nodes);
            //node - discovery time - finished time            
            var expected = new[]
            {
                new { l = "u", d = 1, f = 8 },
                new { l = "v", d = 2, f = 7 },
                new { l = "w", d = 9, f = 12},
                new { l = "x", d = 4, f = 5},
                new { l = "y", d = 3, f = 6},
                new { l = "z", d = 10, f = 11},
            };
            var results = nodes
                .Select(n => new { l = n.Label, d = n.DiscoveredAt, f = n.FinishedAt });

            Assert.IsTrue(expected.SequenceEqual(results));
        }


        /// <summary>
        /// see Ch. 22.3
        /// </summary>
        /// <returns>Nodes with Arcs</returns>
        private DfSearchNode[] GetCormenExample()
        {
            string labels = "uvwxyz";
            var example = labels.Select(l => new DfSearchNode(new string(l, 1)))
                .ToDictionary(n => n.Label);
            string cnx = "uv;ux;xv;vy;yx;wy;wz;zz";
            foreach (var pair in cnx.Split(';'))
                example[pair[0].ToString()].LinkToNode(example[pair[1].ToString()]);

            return example.Values.ToArray();
        }

    }
}
