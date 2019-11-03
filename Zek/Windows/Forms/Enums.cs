using System;

namespace Zek.Windows.Forms
{
    /// <summary>
    /// Edit ფორმის სტილი
    /// </summary>
    [Serializable]
    public enum EditFormStyle { Default, Dialog, }

    /// <summary>
    /// Browse ფორმის სტილი (გრიდზე ორჯერ დაჭერისას რა მოხდეს: გაიხსნას ანჯარა თუ აირჩიოს ჩანაწერი)
    /// </summary>
    [Serializable]
    public enum ListFormStyle { Default, Choose }


    //[Serializable]
    //public enum FormLoadStep
    //{
    //    InitializingComponents = 1,
    //    InitializingPermissions,
    //    BindingFormControls,
    //    BindingData,
    //    EnableDisableFormControls,
    //}

    //[Serializable]
    //public enum InvokeActions { BrowseFormStyle, ChildClosed, MdiParent, SenderHandle, RecordID }
}
