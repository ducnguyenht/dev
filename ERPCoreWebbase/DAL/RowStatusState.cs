using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;

namespace NAS.DAL
{
    public class RowStatusState
    {        
        short RowStatus;
        public RowStatusState()
        {
            RowStatus = Constant.ROWSTATUS_INITIAL;
        }
        public RowStatusState(short _RowStatus)
        {
            RowStatus = _RowStatus;
        }

        public short Transition(short _Transition)
        {
            short result = Constant.ROWSTATUS_TEMP;
            switch (RowStatus)
            {                    
                case Constant.ROWSTATUS_INITIAL:
                    {
                        switch(_Transition)
                        {
                            case Constant.TRANSITION_CREATING:
                                {
                                    result = Constant.ROWSTATUS_TEMP;
                                    break;
                                }
                            case Constant.TRANSITION_POPULATE:
                                {
                                    result = Constant.ROWSTATUS_DEFAULT;
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                        break;
                    }
                case Constant.ROWSTATUS_DEFAULT:
                    {
                        switch (_Transition)
                        {
                            case Constant.TRANSITION_SYSTEM_DELETE:
                                {
                                    result = Constant.ROWSTATUS_FINAL;
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                        break;
                    }
                case Constant.ROWSTATUS_DELETED:
                    {
                        switch (_Transition)
                        {
                            case Constant.TRANSITION_ETL_DELETE:
                                {
                                    result = Constant.ROWSTATUS_OBSOLETE;
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                        break;
                    }
                case Constant.ROWSTATUS_TEMP:
                    {
                        switch (_Transition)
                        {
                            case Constant.TRANSITION_COMPLETE:
                                {
                                    result = Constant.ROWSTATUS_ACTIVE;
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                        break;
                    }
                case Constant.ROWSTATUS_DRILLING_UP_UPDATING:
                    {
                        switch (_Transition)
                        {
                            case Constant.TRANSITION_DRILLING_UP_COMPLETE:
                                {
                                    result = Constant.ROWSTATUS_ACTIVE;
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                        break;
                    }
                case Constant.ROWSTATUS_ETL_UP_UPDATING:
                    {
                        switch (_Transition)
                        {
                            case Constant.TRANSITION_ETL_COMPLETE:
                                {
                                    result = Constant.ROWSTATUS_ACTIVE;
                                    break;
                                }
                            case Constant.TRANSITION_ETL_DELETE:
                                {
                                    result = Constant.ROWSTATUS_OBSOLETE;
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                        break;
                    }
                case Constant.ROWSTATUS_INACTIVE:
                    {
                        switch (_Transition)
                        {
                            case Constant.TRANSITION_USER_ENABLE:
                                {
                                    result = Constant.ROWSTATUS_ACTIVE;
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                        break;
                    }
                case Constant.ROWSTATUS_ACTIVE:
                    {
                        switch (_Transition)
                        {
                            case Constant.TRANSITION_USER_DELETE:
                                {
                                    result = Constant.ROWSTATUS_DELETED;
                                    break;
                                }
                            case Constant.TRANSITION_EDITING:
                                {
                                    result = Constant.ROWSTATUS_TEMP;
                                    break;
                                }
                            case Constant.TRANSITION_DRILLING_UP_UPDATING:
                                {
                                    result = Constant.ROWSTATUS_DRILLING_UP_UPDATING;
                                    break;
                                }
                            case Constant.TRANSITION_USER_DISABLE:
                                {
                                    result = Constant.ROWSTATUS_INACTIVE;
                                    break;
                                }
                            case Constant.TRANSITION_ETL_BEGIN:
                                {
                                    result = Constant.ROWSTATUS_ETL_UP_UPDATING;
                                    break;
                                }
                            case Constant.TRANSITION_SYSTEM_DELETE:
                                {
                                    result = Constant.ROWSTATUS_FINAL;
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                        break;
                    }
                case Constant.ROWSTATUS_OBSOLETE:
                    {
                        switch (_Transition)
                        {
                            case Constant.TRANSITION_SYSTEM_DELETE:
                                {
                                    result = Constant.ROWSTATUS_FINAL;
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                        break;
                    }
                case Constant.ROWSTATUS_DISABLE:
                    {
                        result = Constant.ROWSTATUS_DISABLE;
                        break;
                    }
                default: break;
            }
            return result;
        }
    }
}
