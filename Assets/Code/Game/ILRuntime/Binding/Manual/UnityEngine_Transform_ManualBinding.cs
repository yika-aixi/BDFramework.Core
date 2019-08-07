
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

using ILRuntime.CLR.TypeSystem;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Stack;
using ILRuntime.Reflection;
using ILRuntime.CLR.Utils;
using UnityEngine;

namespace ILRuntime.Runtime.Generated
{
    unsafe class UnityEngine_Transform_ManualBinding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            var arr = typeof(Transform).GetMethods();
            foreach (var i in arr)
            {
                if (i.Name == "GetComponent" && i.GetGenericArguments().Length == 1)
                {
                   // app.RegisterCLRMethodRedirection(i, GetComponent);
                }
            }
            
            

        }


        unsafe static StackObject* GetComponent(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            //CLR�ض����˵���뿴����ĵ��ͽ̳̣����ﲻ��������
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;

            var ptr = __esp - 1;
            //��Ա�����ĵ�һ������Ϊthis
            Transform instance = StackObject.ToObject(ptr, __domain, __mStack) as Transform;
            if (instance == null)
                throw new System.NullReferenceException();
            __intp.Free(ptr);

            var genericArgument = __method.GenericArguments;
            //GetComponentӦ������ֻ��1�����Ͳ���
            if (genericArgument != null && genericArgument.Length == 1)
            {
                var type = genericArgument[0];
                object res = null;
                if (type is CLRType)
                {
                    //Unity�����̵��಻��Ҫ�κ����⴦��ֱ�ӵ���Unity�ӿ�
                    res = instance.GetComponent(type.TypeForCLR);
                }
                else
                {
                    //��Ϊ����DLL�����MonoBehaviourʵ�ʶ������Component����������ֻ��ȫȡ������������
                    var clrInstances = instance.GetComponents<MonoBehaviourAdapter.Adaptor>();
                    for(int i = 0; i < clrInstances.Length; i++)
                    {
                        var clrInstance = clrInstances[i];
                        if (clrInstance.ILInstance != null)//ILInstanceΪnull, ��ʾ����Ч��MonoBehaviour��Ҫ�Թ�
                        {
                            if (clrInstance.ILInstance.Type == type)
                            {
                                res = clrInstance.ILInstance;//����ILRuntime��ʵ��Ӧ��ΪILInstance
                                break;
                            }
                        }
                    }
                }

                return ILIntepreter.PushObject(ptr, __mStack, res);
            }

            return __esp;
        }
       
    }
}
