using ElmahCore;

namespace DMBolsaTrabajo.Utilitarios
{
    public static class LogDeError
    {
        /// <summary>
        /// Log error a Elmah
        /// </summary>
        public static void GestorError(Exception ex, string contextualMessage = null)
        {
            try
            {
                // log error to Elmah
                if (contextualMessage != null)
                {
                    // log exception with contextual information that's visible when 
                    // clicking on the error in the Elmah log
                    var annotatedException = new Exception(contextualMessage, ex);
                    //ErrorSignal.FromCurrentContext().Raise(annotatedException, HttpContext.Current);
                    ElmahExtensions.RaiseError(annotatedException);
                }
                else
                {
                    ElmahExtensions.RaiseError(ex);
                    //ErrorSignal.FromCurrentContext().Raise(ex, HttpContext.Current);
                }

                // send errors to ErrorWS (my own legacy service)
                // using (ErrorWSSoapClient client = new ErrorWSSoapClient())
                // {
                //    client.LogErrors(...);
                // }
            }
            catch (Exception)
            {
                // uh oh! just keep going
            }
        }

        //public static void GestorSeguimiento(Exception ex, string contextualMessage = null)
        //{
        //    try
        //    {
        //        // log error to Elmah
        //        if (contextualMessage != null)
        //        {
        //            // log exception with contextual information that's visible when 
        //            // clicking on the error in the Elmah log
        //            var annotatedException = new Exception(contextualMessage, ex);
        //            //ErrorSignal.FromCurrentContext().Raise(annotatedException, HttpContext.Current);
        //            ElmahExtensions.RaiseError(annotatedException);
        //        }
        //        else
        //        {
        //            ElmahExtensions.RaiseError(ex);
        //            //ErrorSignal.FromCurrentContext().Raise(ex, HttpContext.Current);
        //        }

        //        // send errors to ErrorWS (my own legacy service)
        //        // using (ErrorWSSoapClient client = new ErrorWSSoapClient())
        //        // {
        //        //    client.LogErrors(...);
        //        // }
        //    }
        //    catch (Exception)
        //    {
        //        // uh oh! just keep going
        //    }
        //}
    }
}