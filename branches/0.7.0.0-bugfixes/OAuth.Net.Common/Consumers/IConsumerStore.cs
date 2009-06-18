﻿// Copyright (c) 2008 Madgex
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// 
// OAuth.net uses the Common Service Locator interface, released under the MS-PL
// license. See "CommonServiceLocator License.txt" in the Licenses folder.
// 
// The examples and test cases use the Windsor Container from the Castle Project
// and Common Service Locator Windsor adaptor, released under the Apache License,
// Version 2.0. See "Castle Project License.txt" in the Licenses folder.
// 
// XRDS-Simple.net uses the HTMLAgility Pack. See "HTML Agility Pack License.txt"
// in the Licenses folder.
//
// Authors: Bruce Boughton, Chris Adams
// Website: http://lab.madgex.com/oauth-net/
// Email:   oauth-dot-net@madgex.com

using System.Collections.Generic;

namespace OAuth.Net.Common
{
    public interface IConsumerStore
    {
        /// <summary>
        /// Gets all the consumers in the store.
        /// </summary>
        ICollection<IConsumer> Consumers
        {
            get;
        }

        /// <summary>
        /// The number of consumers held in the store
        /// </summary>
        long Count
        {
            get;
        }

        /// <summary>
        /// Adds the supplied consumer to the consumer store. If the consumer conflicts with a 
        /// consumer already in the store, then <c>false</c> is returned.
        /// </summary>
        /// <param name="consumer">The consumer to store</param>
        /// <returns><c>true</c>, iff the consumer was stored</returns>
        bool Add(IConsumer consumer);

        /// <summary>
        /// Checks whether the store contains a consumer with the given
        /// consumer key.
        /// 
        /// This will return true even if the consumer has a status other than <c>Valid</c>.
        /// </summary>
        /// <param name="consumerKey">The consumer key</param>
        /// <returns><c>true</c>, iff there is such a consumer 
        /// in the store</returns>
        bool Contains(string consumerKey);

        /// <summary>
        /// Gets the consumer registered with the given
        /// consumer key.
        /// </summary>
        /// <param name="consumerKey">The consumer key</param>
        /// <returns>The consumer registered with the consumer key if successful, or
        /// <c>null</c> if there is no consumer registered with the supplied key</returns>
        IConsumer GetByKey(string consumerKey);

        /// <summary>
        /// Updates the given consumer in the store.
        /// </summary>
        /// <param name="consumer">The consumer to update.</param>
        /// <returns><c>true</c>, iff the consumer was successfully updated
        /// in the store</returns>
        bool Update(IConsumer consumer);

        /// <summary>
        /// Removes the specified consumer from the store.
        /// </summary>
        /// <param name="consumer">The consumer to remove</param>
        /// <returns><c>true</c>, iff the consumer was successfully removed
        /// from the store. This will return <c>false</c> if the consumer 
        /// did not exist in the store.</returns>
        bool Remove(IConsumer consumer);
    }
}
