using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KEE.HyMotion.Common
{
    public class FormFactory
    {
        private static List<Form> forms = new List<Form>();
        private static List<Type> types;
        static FormFactory()
        {
            Assembly ass = Assembly.LoadFrom("KEE.HyMotion.exe");
            types = ass.GetTypes().ToList();
        }
        public static Form CreateForm(string formName)
        {
            HideFormAll();
            formName = formName == null ? "FrmNone" : formName;
            Form form = forms.Find(m => m.Name == formName);
            if (form == null)
            {
                Type type = types.Find(m => m.Name == formName);
                form = (Form)Activator.CreateInstance(type);
                forms.Add(form);
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
