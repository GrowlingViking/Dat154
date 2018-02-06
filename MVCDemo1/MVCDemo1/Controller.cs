using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace MVCDemo1
{
    delegate void UpdateView(int updateV);
 
    class Controller
    {
        static UpdateView updateV;
        public Controller() {}

        static public void SetModel()
        {
        }

        // Register the view
        static public void Register(IView iv)
        {
            updateV += iv.UpdateView;
        }

        // Called from the view - update model
        static public void Update(int val)
        {
            Model.Update(val);
   
        }

        // Called when model has been updated
        static public void Notify()
        {
            if (updateV != null) updateV.Invoke(Model.value);
        }

        // Get the model data
        static public int GetValue()
        {
            return Model.GetValue();
        }

  
        // Return the history of the sales
        static public List<int> History()
        {
            return Model.a;
        }
    }

  
}
