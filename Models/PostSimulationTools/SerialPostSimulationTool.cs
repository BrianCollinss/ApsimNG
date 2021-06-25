using Models.Core;
using Models.Core.Run;
using Models.Storage;

namespace Models.PostSimulationTools
{
    /// <summary>
    /// This is a post-simulation tool which will run all child post-simulation
    /// tools in parallel.
    /// </summary>
    [ValidParent(typeof(IDataStore))]
    [ValidParent(typeof(ParallelPostSimulationTool))]
    [ValidParent(typeof(SerialPostSimulationTool))]
    public class SerialPostSimulationTool : Model, IPostSimulationTool
    {
        /// <summary>
        /// Run the post-simulation tool.
        /// </summary>
        public void Run()
        {
            foreach (IPostSimulationTool tool in FindAllChildren<IPostSimulationTool>())
            {
                new Links(new object[1] { FindInScope<IDataStore>() }).Resolve(tool);
                tool.Run();
            }
        }
    }
}
