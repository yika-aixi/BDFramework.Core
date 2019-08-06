//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年08月07日-00:39
//Assembly-CSharp

using System.Reflection;
using BDFramework.UI;
using UnityEngine.UI;

namespace Code.BDFramework.Core.Tools
{
    public static class IViewExtension
    {
        public static void ModelBind(this FieldInfo self,object view,object value)
        {
            var com = self.GetValue(view);
            switch (com)
            {
                case  Text text:
                    text.text = value.ToString();
                    break;    
                case Slider slider:
                    slider.value = (float) value;
                    break;
                case Scrollbar scrollbar:
                    scrollbar.value = (float) value;
                    break;
                case Toggle toggle:
                    toggle.isOn = (bool)value;
                    break;
                default:
                    BDebug.LogError("不支持类型,请扩展：" + self.Name  + "-"+ view.GetType().FullName);
                    break;
                                
            }
        }
    }
}