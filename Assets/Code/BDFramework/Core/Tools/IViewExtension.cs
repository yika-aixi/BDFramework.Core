//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年08月07日-00:39
//Assembly-CSharp

using System.Reflection;
using BDFramework.UI;
using Game.UI;
using UnityEngine.UI;

namespace Code.BDFramework.Core.Tools
{
    public static class IViewExtension
    {
        public static void ModelBind(this IView self,FieldInfo fieldInfo,object value)
        {
            ModelBind(fieldInfo, self, value);
        }

        public static void MethodBind(this IView self, FieldInfo fieldInfo, IViewControl viewControl)
        {
            MethodBind(fieldInfo, self, viewControl);
        }

        public static void MethodBind(FieldInfo fieldInfo,object view, object viewControl)
        {
            var controlType = viewControl.GetType();

            var com = fieldInfo.GetValue(view);
            MethodInfo methodInfo;
            switch (com)
            {
                case Button button :
                    methodInfo = controlType.GetMethod(string.Format("OnClick_{0}", fieldInfo.Name ),BindingFlags.Instance | BindingFlags.NonPublic);
                    
                    if (methodInfo!=null)
                    {
                        button.onClick.AddListener(() =>
                        {
                            methodInfo.Invoke(viewControl, new object[]{});
                        });
                        return;
                    }
                    
                    break;
                case Scrollbar scrollbar :
                    
                    methodInfo = controlType.GetMethod(string.Format("OnValueChange_{0}", fieldInfo.Name),BindingFlags.Instance | BindingFlags.NonPublic);
                    
                    if (methodInfo != null)
                    {
                       
                        scrollbar.onValueChanged.AddListener((value) =>
                        {
                            methodInfo.Invoke(viewControl, new object[] {value});
                        });
                        return;
                    }
                   
                    break;
                case Slider slider :
                    methodInfo = controlType.GetMethod(string.Format("OnValueChange_{0}", fieldInfo.Name),BindingFlags.Instance | BindingFlags.NonPublic);
                    
                    if (methodInfo != null)
                    {
                        slider.onValueChanged.AddListener((value) =>
                        {
                            methodInfo.Invoke(viewControl, new object[] {value});
                        });
                    
                        return;
                    }
                    
                    break;
            }
            
            BDebug.Log(string.Format("ui事件未实现:{0} - {1}" ,fieldInfo.GetType().FullName , fieldInfo.Name )  , "yellow");
        }

        public static void ModelBind(FieldInfo fieldInfo,object view,object value)
        {
            var com = fieldInfo.GetValue(view);
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
                    BDebug.LogError("不支持类型,请扩展：" + fieldInfo.Name  + "-"+ view.GetType().FullName);
                    break;
                                
            }
        }
    }
}