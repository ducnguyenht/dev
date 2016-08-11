using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.ERPSystem.CustomField.State.CustomFieldEditingForm.Transition
{

    public enum CustomFieldCreatingTransition
    {
        HAS_INIT_DATA,
        HAS_NO_INIT_DATA,
        CANCEL
    }

    public enum CustomFieldDataCreatingTransition
    {
        BACK,
        CANCEL,
        ACCEPT,
        CREATE_SINGLE_CHOICE_LIST_DATA,
        CREATE_MULTI_CHOICE_LIST_DATA
    }

    public enum CustomFieldCreatingHasInitDataTransition
    {
        CANCEL,
        NEXT,
        HAS_NO_INIT_DATA,
        HAS_INIT_DATA
    }

    public enum CustomFieldCreatingHasNoInitDataTransition
    {
        CANCEL,
        ACCEPT,
        HAS_INIT_DATA,
        HAS_NO_INIT_DATA
    }




    public enum CustomFieldEditingTransition
    {
        HAS_INIT_DATA,
        HAS_NO_INIT_DATA,
        CANCEL,
        SAVE,
        EDIT_DATA
    }

    public enum CustomFieldEditingHasInitDataTransition
    {
        CANCEL,
        HAS_INIT_DATA,
        HAS_NO_INIT_DATA,
        SAVE,
        EDIT_DATA
    }

    public enum CustomFieldEditingHasNoInitDataTransition
    {
        CANCEL,
        HAS_INIT_DATA,
        HAS_NO_INIT_DATA,
        SAVE
    }

    public enum CustomFieldDataEditingTransition
    {
        CANCEL,
        EDIT_SINGLE_CHOICE_LIST_DATA,
        EDIT_MULTI_CHOICE_LIST_DATA,
        EDIT_FIELD
    }

}