﻿using System;

namespace Zek.Core
{
    public abstract class BaseStaticOverride<T> where T : BaseStaticOverride<T>
    {
        private static T _instance;
        private static readonly object Lock = new object();

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (Lock)
                    {
                        if (_instance == null)
                            _instance = (T)Activator.CreateInstance(typeof(T), true);
                    }
                }
                return _instance;
            }
        }
    }
    /*
abstract class C1Base<T> : BaseStaticOverride<T> where T : C1Base<T>
{
    protected C1Base()
    {
    }
 
    public void WriteLine()
    {
        Console.WriteLine(Line);
    }
    protected abstract string Line
    {
        get;
    }
}
class C1 : C1Base<C1>
{
    private C1()
    {
    }
 
    protected override string Line
    {
        get { return "C1"; }
    }
}
class C2 : C1Base<C2>
{
    private C2()
    {
    }
 
    protected override string Line
    {
        get { return "C2"; }
    }
}
     */
}
