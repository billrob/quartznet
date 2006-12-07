/* 
* Copyright 2004-2005 OpenSymphony 
* 
* Licensed under the Apache License, Version 2.0 (the "License"); you may not 
* use this file except in compliance with the License. You may obtain a copy 
* of the License at 
* 
*   http://www.apache.org/licenses/LICENSE-2.0 
*   
* Unless required by applicable law or agreed to in writing, software 
* distributed under the License is distributed on an "AS IS" BASIS, WITHOUT 
* WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the 
* License for the specific language governing permissions and limitations 
* under the License.
* 
*/

/*
* Previously Copyright (c) 2001-2004 James House
*/
using System;
using log4net;
using Quartz.Spi;

namespace Quartz.Plugins.History
{
	/// <summary>
	///  Logs a history of all job executions (and execution vetos) via log4net.
	/// 
	/// <p>
	/// The logged message is customizable by setting one of the following message
	/// properties to a string that conforms to the syntax of <code>java.util.MessageFormat</code>.
	/// </p>
	/// 
	/// <p>
	/// JobToBeFiredMessage - available message data are: <table>
	/// <tr>
	/// <th>Element</th>
	/// <th>Data Type</th>
	/// <th>Description</th>
	/// </tr>
	/// <tr>
	/// <td>0</td>
	/// <td>String</td>
	/// <td>The Job's Name.</td>
	/// </tr>
	/// <tr>
	/// <td>1</td>
	/// <td>String</td>
	/// <td>The Job's Group.</td>
	/// </tr>
	/// <tr>
	/// <td>2</td>
	/// <td>Date</td>
	/// <td>The current time.</td>
	/// </tr>
	/// <tr>
	/// <td>3</td>
	/// <td>String</td>
	/// <td>The Trigger's name.</td>
	/// </tr>
	/// <tr>
	/// <td>4</td>
	/// <td>String</td>
	/// <td>The Triggers's group.</td>
	/// </tr>
	/// <tr>
	/// <td>5</td>
	/// <td>Date</td>
	/// <td>The scheduled fire time.</td>
	/// </tr>
	/// <tr>
	/// <td>6</td>
	/// <td>Date</td>
	/// <td>The next scheduled fire time.</td>
	/// </tr>
	/// <tr>
	/// <td>7</td>
	/// <td>Integer</td>
	/// <td>The re-fire count from the JobExecutionContext.</td>
	/// </tr>
	/// </table>
	/// 
	/// The default message text is <i>"Job {1}.{0} fired (by trigger {4}.{3}) at:
	/// {2, date, HH:mm:ss MM/dd/yyyy}"</i>
	/// </p>
	/// 
	/// 
	/// <p>
	/// JobSuccessMessage - available message data are: <table>
	/// <tr>
	/// <th>Element</th>
	/// <th>Data Type</th>
	/// <th>Description</th>
	/// </tr>
	/// <tr>
	/// <td>0</td>
	/// <td>String</td>
	/// <td>The Job's Name.</td>
	/// </tr>
	/// <tr>
	/// <td>1</td>
	/// <td>String</td>
	/// <td>The Job's Group.</td>
	/// </tr>
	/// <tr>
	/// <td>2</td>
	/// <td>Date</td>
	/// <td>The current time.</td>
	/// </tr>
	/// <tr>
	/// <td>3</td>
	/// <td>String</td>
	/// <td>The Trigger's name.</td>
	/// </tr>
	/// <tr>
	/// <td>4</td>
	/// <td>String</td>
	/// <td>The Triggers's group.</td>
	/// </tr>
	/// <tr>
	/// <td>5</td>
	/// <td>Date</td>
	/// <td>The scheduled fire time.</td>
	/// </tr>
	/// <tr>
	/// <td>6</td>
	/// <td>Date</td>
	/// <td>The next scheduled fire time.</td>
	/// </tr>
	/// <tr>
	/// <td>7</td>
	/// <td>Integer</td>
	/// <td>The re-fire count from the JobExecutionContext.</td>
	/// </tr>
	/// <tr>
	/// <td>8</td>
	/// <td>Object</td>
	/// <td>The string value (toString() having been called) of the result (if any) 
	/// that the Job set on the JobExecutionContext, with on it.  "NULL" if no 
	/// result was set.</td>
	/// </tr>
	/// </table>
	/// 
	/// The default message text is <i>"Job {1}.{0} execution complete at {2, date,
	/// HH:mm:ss MM/dd/yyyy} and reports: {8}"</i>
	/// </p>
	/// 
	/// <p>
	/// JobFailedMessage - available message data are: <table>
	/// <tr>
	/// <th>Element</th>
	/// <th>Data Type</th>
	/// <th>Description</th>
	/// </tr>
	/// <tr>
	/// <td>0</td>
	/// <td>String</td>
	/// <td>The Job's Name.</td>
	/// </tr>
	/// <tr>
	/// <td>1</td>
	/// <td>String</td>
	/// <td>The Job's Group.</td>
	/// </tr>
	/// <tr>
	/// <td>2</td>
	/// <td>Date</td>
	/// <td>The current time.</td>
	/// </tr>
	/// <tr>
	/// <td>3</td>
	/// <td>String</td>
	/// <td>The Trigger's name.</td>
	/// </tr>
	/// <tr>
	/// <td>4</td>
	/// <td>String</td>
	/// <td>The Triggers's group.</td>
	/// </tr>
	/// <tr>
	/// <td>5</td>
	/// <td>Date</td>
	/// <td>The scheduled fire time.</td>
	/// </tr>
	/// <tr>
	/// <td>6</td>
	/// <td>Date</td>
	/// <td>The next scheduled fire time.</td>
	/// </tr>
	/// <tr>
	/// <td>7</td>
	/// <td>Integer</td>
	/// <td>The re-fire count from the JobExecutionContext.</td>
	/// </tr>
	/// <tr>
	/// <td>8</td>
	/// <td>String</td>
	/// <td>The message from the thrown JobExecution Exception.
	/// </td>
	/// </tr>
	/// </table>
	/// 
	/// The default message text is <i>"Job {1}.{0} execution failed at {2, date,
	/// HH:mm:ss MM/dd/yyyy} and reports: {8}"</i>
	/// </p>
	/// 
	/// 
	/// <p>
	/// JobWasVetoedMessage - available message data are: <table>
	/// <tr>
	/// <th>Element</th>
	/// <th>Data Type</th>
	/// <th>Description</th>
	/// </tr>
	/// <tr>
	/// <td>0</td>
	/// <td>String</td>
	/// <td>The Job's Name.</td>
	/// </tr>
	/// <tr>
	/// <td>1</td>
	/// <td>String</td>
	/// <td>The Job's Group.</td>
	/// </tr>
	/// <tr>
	/// <td>2</td>
	/// <td>Date</td>
	/// <td>The current time.</td>
	/// </tr>
	/// <tr>
	/// <td>3</td>
	/// <td>String</td>
	/// <td>The Trigger's name.</td>
	/// </tr>
	/// <tr>
	/// <td>4</td>
	/// <td>String</td>
	/// <td>The Triggers's group.</td>
	/// </tr>
	/// <tr>
	/// <td>5</td>
	/// <td>Date</td>
	/// <td>The scheduled fire time.</td>
	/// </tr>
	/// <tr>
	/// <td>6</td>
	/// <td>Date</td>
	/// <td>The next scheduled fire time.</td>
	/// </tr>
	/// <tr>
	/// <td>7</td>
	/// <td>Integer</td>
	/// <td>The re-fire count from the JobExecutionContext.</td>
	/// </tr>
	/// </table>
	/// 
	/// The default message text is <i>"Job {1}.{0} was vetoed.  It was to be fired 
	/// (by trigger {4}.{3}) at: {2, date, HH:mm:ss MM/dd/yyyy}"</i>
	/// </p>
	/// </summary>
	/// <author>James House</author>
	public class LoggingJobHistoryPlugin : ISchedulerPlugin, IJobListener
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof (LoggingJobHistoryPlugin));

		/// <summary> 
		/// Get or sets the message that is logged when a Job successfully completes its 
		/// execution.
		/// </summary>
		public virtual string JobSuccessMessage
		{
			get { return jobSuccessMessage; }
			set { jobSuccessMessage = value; }
		}

		/// <summary> 
		/// Get or sets the message that is logged when a Job fails its 
		/// execution.
		/// </summary>
		public virtual string JobFailedMessage
		{
			get { return jobFailedMessage; }
			set { jobFailedMessage = value; }
		}

		/// <summary> 
		/// Gets or sets the message that is logged when a Job is about to Execute.
		/// </summary>
		public virtual string JobToBeFiredMessage
		{
			get { return jobToBeFiredMessage; }
			set { jobToBeFiredMessage = value; }
		}

		/// <summary> 
		/// Gets or sets the message that is logged when a Job execution is vetoed by a
		/// trigger listener.
		/// </summary>
		public virtual string JobWasVetoedMessage
		{
			get { return jobWasVetoedMessage; }
			set { jobWasVetoedMessage = value; }
		}

		public virtual string Name
		{
			/*
			* object[] arguments = { new Integer(7), new
			* Date(System.currentTimeMillis()), "a disturbance in the Force" };
			* 
			* string result = MessageFormat.format( "At {1,time} on {1,date}, there
			* was {2} on planet {0,number,integer}.", arguments);
			*/


			get { return name; }
		}

		private string name;
		private string jobToBeFiredMessage = "Job {1}.{0} fired (by trigger {4}.{3}) at: {2, date, HH:mm:ss MM/dd/yyyy}";
		private string jobSuccessMessage = "Job {1}.{0} execution complete at {2, date, HH:mm:ss MM/dd/yyyy} and reports: {8}";
		private string jobFailedMessage = "Job {1}.{0} execution failed at {2, date, HH:mm:ss MM/dd/yyyy} and reports: {8}";

		private string jobWasVetoedMessage =
			"Job {1}.{0} was vetoed.  It was to be fired (by trigger {4}.{3}) at: {2, date, HH:mm:ss MM/dd/yyyy}";

		/// <summary>
		/// Called during creation of the <code>Scheduler</code> in order to give
		/// the <code>SchedulerPlugin</code> a chance to initialize.
		/// </summary>
		public virtual void Initialize(String pluginName, IScheduler sched)
		{
			name = pluginName;
			sched.AddGlobalJobListener(this);
		}

		public virtual void Start()
		{
			// do nothing...
		}

		/// <summary> 
		/// Called in order to inform the <code>SchedulerPlugin</code> that it
		/// should free up all of it's resources because the scheduler is shutting
		/// down.
		/// </summary>
		public virtual void Shutdown()
		{
			// nothing to do...
		}

		public virtual void JobToBeExecuted(JobExecutionContext context)
		{
			if (!Log.IsInfoEnabled)
			{
				return;
			}

			Trigger trigger = context.Trigger;

			object[] args =
				new object[]
					{
						context.JobDetail.Name, context.JobDetail.Group, DateTime.Now, trigger.Name, trigger.Group,
						trigger.GetPreviousFireTime(), trigger.GetNextFireTime(), context.RefireCount
					};

			Log.Info(String.Format(JobToBeFiredMessage, args));
		}


		public virtual void JobWasExecuted(JobExecutionContext context, JobExecutionException jobException)
		{
			Trigger trigger = context.Trigger;

			object[] args;

			if (jobException != null)
			{
				if (!Log.IsWarnEnabled)
				{
					return;
				}

				string errMsg = jobException.Message;
				args =
					new object[]
						{
							context.JobDetail.Name, context.JobDetail.Group, DateTime.Now, trigger.Name, trigger.Group,
							trigger.GetPreviousFireTime(), trigger.GetNextFireTime(), context.RefireCount, errMsg
						};

				Log.Warn(String.Format(JobFailedMessage, args), jobException);
			}
			else
			{
				if (!Log.IsInfoEnabled)
				{
					return;
				}

				string result = Convert.ToString(context.Result);
				args =
					new object[]
						{
							context.JobDetail.Name, context.JobDetail.Group, DateTime.Now, trigger.Name, trigger.Group,
							trigger.GetPreviousFireTime(), trigger.GetNextFireTime(), context.RefireCount, result
						};

				Log.Info(String.Format(JobSuccessMessage, args));
			}
		}

		public virtual void JobExecutionVetoed(JobExecutionContext context)
		{
			if (!Log.IsInfoEnabled)
			{
				return;
			}

			Trigger trigger = context.Trigger;

			object[] args =
				new object[]
					{
						context.JobDetail.Name, context.JobDetail.Group, DateTime.Now, trigger.Name, trigger.Group,
						trigger.GetPreviousFireTime(), trigger.GetNextFireTime(), context.RefireCount
					};

			Log.Info(String.Format(JobWasVetoedMessage, args));
		}
	}
}