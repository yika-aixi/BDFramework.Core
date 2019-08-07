using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

using ILRuntime.CLR.TypeSystem;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Stack;
using ILRuntime.Reflection;
using ILRuntime.CLR.Utils;

namespace ILRuntime.Runtime.Generated
{
    unsafe class BDFramework_ResourceMgr_BResources_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            Type[] args;
            Type type = typeof(BDFramework.ResourceMgr.BResources);
            Dictionary<string, List<MethodInfo>> genericMethods = new Dictionary<string, List<MethodInfo>>();
            List<MethodInfo> lst = null;                    
            foreach(var m in type.GetMethods())
            {
                if(m.IsGenericMethodDefinition)
                {
                    if (!genericMethods.TryGetValue(m.Name, out lst))
                    {
                        lst = new List<MethodInfo>();
                        genericMethods[m.Name] = lst;
                    }
                    lst.Add(m);
                }
            }
            args = new Type[]{typeof(UnityEngine.GameObject)};
            if (genericMethods.TryGetValue("AsyncLoad", out lst))
            {
                foreach(var m in lst)
                {
                    if(m.MatchGenericParameters(args, typeof(System.Int32), typeof(System.String), typeof(System.Action<System.Boolean, UnityEngine.GameObject>)))
                    {
                        method = m.MakeGenericMethod(args);
                        app.RegisterCLRMethodRedirection(method, AsyncLoad_0);

                        break;
                    }
                }
            }
            args = new Type[]{typeof(UnityEngine.GameObject)};
            if (genericMethods.TryGetValue("Load", out lst))
            {
                foreach(var m in lst)
                {
                    if(m.MatchGenericParameters(args, typeof(UnityEngine.GameObject), typeof(System.String)))
                    {
                        method = m.MakeGenericMethod(args);
                        app.RegisterCLRMethodRedirection(method, Load_1);

                        break;
                    }
                }
            }
            args = new Type[]{typeof(UnityEngine.Transform)};
            method = type.GetMethod("Destroy", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Destroy_2);
            args = new Type[]{typeof(System.String), typeof(System.Boolean)};
            method = type.GetMethod("UnloadAsset", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, UnloadAsset_3);
            args = new Type[]{typeof(UnityEngine.Sprite)};
            if (genericMethods.TryGetValue("Load", out lst))
            {
                foreach(var m in lst)
                {
                    if(m.MatchGenericParameters(args, typeof(UnityEngine.Sprite), typeof(System.String)))
                    {
                        method = m.MakeGenericMethod(args);
                        app.RegisterCLRMethodRedirection(method, Load_4);

                        break;
                    }
                }
            }
            args = new Type[]{typeof(System.Collections.Generic.IList<System.String>), typeof(System.Action<System.Int32, System.Int32>), typeof(System.Action<System.Collections.Generic.IDictionary<System.String, UnityEngine.Object>>)};
            method = type.GetMethod("AsyncLoad", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, AsyncLoad_5);


        }


        static StackObject* AsyncLoad_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Action<System.Boolean, UnityEngine.GameObject> @action = (System.Action<System.Boolean, UnityEngine.GameObject>)typeof(System.Action<System.Boolean, UnityEngine.GameObject>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.String @objName = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);


            var result_of_this_method = BDFramework.ResourceMgr.BResources.AsyncLoad<UnityEngine.GameObject>(@objName, @action);

            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static StackObject* Load_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.String @name = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);


            var result_of_this_method = BDFramework.ResourceMgr.BResources.Load<UnityEngine.GameObject>(@name);

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* Destroy_2(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityEngine.Transform @trans = (UnityEngine.Transform)typeof(UnityEngine.Transform).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);


            BDFramework.ResourceMgr.BResources.Destroy(@trans);

            return __ret;
        }

        static StackObject* UnloadAsset_3(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Boolean @isUnloadIsUsing = ptr_of_this_method->Value == 1;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.String @path = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);


            BDFramework.ResourceMgr.BResources.UnloadAsset(@path, @isUnloadIsUsing);

            return __ret;
        }

        static StackObject* Load_4(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.String @name = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);


            var result_of_this_method = BDFramework.ResourceMgr.BResources.Load<UnityEngine.Sprite>(@name);

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* AsyncLoad_5(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 3);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Action<System.Collections.Generic.IDictionary<System.String, UnityEngine.Object>> @onLoadEnd = (System.Action<System.Collections.Generic.IDictionary<System.String, UnityEngine.Object>>)typeof(System.Action<System.Collections.Generic.IDictionary<System.String, UnityEngine.Object>>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.Action<System.Int32, System.Int32> @onProcess = (System.Action<System.Int32, System.Int32>)typeof(System.Action<System.Int32, System.Int32>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            System.Collections.Generic.IList<System.String> @objlist = (System.Collections.Generic.IList<System.String>)typeof(System.Collections.Generic.IList<System.String>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);


            var result_of_this_method = BDFramework.ResourceMgr.BResources.AsyncLoad(@objlist, @onProcess, @onLoadEnd);

            object obj_result_of_this_method = result_of_this_method;
            if(obj_result_of_this_method is CrossBindingAdaptorType)
            {    
                return ILIntepreter.PushObject(__ret, __mStack, ((CrossBindingAdaptorType)obj_result_of_this_method).ILInstance);
            }
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }



    }
}
