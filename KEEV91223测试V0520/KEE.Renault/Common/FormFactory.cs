using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KEE.Renault.Common
{
    public class FormFactory
    {
        public static List<Form> forms = new List<Form>();
        private static List<Type> types;
        static FormFactory()
        {
            Assembly ass = Assembly.LoadFrom("KEE.Renault.exe");
            types = ass.GetTypes().ToList();
        }
        public static Form CreateForm(string formName)
        {
            //if (formName != "FrmEpsonRobot" && formName != "FrmFeedAxias" && formName != "FrmFeedFromInjectAxias" && formName != "FrmFeedRAxias" && formName != "FrmLabelingAxias" && formName != "FrmTakeAxias")
            //{
            //    HideFormAll();
            //}
            HideFormAll();
            formName = formName == null ? "FrmNone" : formName;
            Form form = forms.Find(m => m.Name == formName);
            if (form == null)
            {
                Type type = types.Find(m => m.Name == formName);

                form = (Form)Activator.CreateInstance(type);
                forms.Add(form);
            }
            if (formName == "FrmEpsonRobot" || formName == "FrmFeedAxias" || formName == "FrmFeedFromInjectAxias" || formName == "FrmFeedRAxias" || formName == "FrmLabelingAxias" || formName == "FrmTakeAxias" || formName == "FrmPressAltimetry" || formName == "FrmSplitter")
            {
                forms.FirstOrDefault(t => t.Name == "FrmAxiasMain").Show();
            }
            return form;
        }
        public static void HideFormAll()
        {
            foreach (var form in forms)
            {
                form.Hide();
            }
        }
    }
}
