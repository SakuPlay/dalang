using System.Collections.Generic;
using UnityEngine;

namespace Sakuplay.Narradho.Views
{
    public class InputLinePeg:MonoBehaviour
    {
  
        
        public List<OutputLinePeg> Connections = new List<OutputLinePeg>();

        public bool AllowToConnect(OutputLinePeg outputPeg)
        {
            return !Connections.Contains(outputPeg);
        }
        
        public void Connect(OutputLinePeg outputPeg)
        {
            if (AllowToConnect(outputPeg)) Connections.Add(outputPeg);
        }

        public void UpdateLines()
        {
            for (int i = 0; i < Connections.Count; i++)
            {
                var connection = Connections[i];
                connection.UpdateLines();
            }
        }
    }
}