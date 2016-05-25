﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace SimpleWhiteFixture.xUnit.Impl
{
    internal class XunitSkippedDataRowTestCase : XunitTestCase
    {
        readonly string skipReason;

        /// <summary/>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Called by the de-serializer; should only be called by deriving classes for de-serialization purposes")]
        public XunitSkippedDataRowTestCase()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XunitSkippedDataRowTestCase"/> class.
        /// </summary>
        /// <param name="diagnosticMessageSink">The message sink used to send diagnostic messages</param>
        /// <param name="defaultMethodDisplay">Default method display to use (when not customized).</param>
        /// <param name="testMethod">The test method this test case belongs to.</param>
        /// <param name="skipReason">The reason that this test case will be skipped</param>
        /// <param name="testMethodArguments">The arguments for the test method.</param>
        public XunitSkippedDataRowTestCase(IMessageSink diagnosticMessageSink, TestMethodDisplay defaultMethodDisplay, ITestMethod testMethod, string skipReason, object[] testMethodArguments = null) :
            base(diagnosticMessageSink, defaultMethodDisplay, testMethod, testMethodArguments)
        {
            this.skipReason = skipReason;
        }

        /// <summary>
        /// Gets the skip reason for the test case. Overrides the default to use the skip reason from the 
        /// value found during discovery
        /// </summary>
        /// <param name="factAttribute">The fact attribute the decorated the test case.</param>
        /// <returns>The skip reason, if skipped; <c>null</c>, otherwise.</returns>
        protected override string GetSkipReason(IAttributeInfo factAttribute)
        {
            return skipReason;
        }
    }
}
