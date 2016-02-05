﻿/*
The MIT License (MIT)

Copyright (c) 2015 Denis Lebedev

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
 */

using System;
using System.Collections.Generic;
using System.Reflection;

namespace QuikConnector.Data
{
    public class DataTable<T> : DataChannel, QuikConnector.Data.IDataTable<T> where T : new()
    {
        PropertyInfo[] properties;

        public List<T> Rows { get; protected set; }

        public event EventHandler<List<T>> Updated;

        public DataTable()
        {
            properties = typeof(T).GetProperties();

            Rows = new List<T>();
        }

        protected override void ProcessTable(XlTable xt)
        {
            Rows.Clear();

            for (int row = 0; row < xt.Rows; row++)
            {
                T item = new T();

                for (int col = 0; col < xt.Columns; col++)
                {
                    xt.ReadValue();

                    switch (xt.ValueType)
                    {
                        case XlTable.BlockType.Float:
                            properties[col].SetValue(item, (decimal)xt.FloatValue);
                            break;

                        case XlTable.BlockType.String:
                            if (xt.StringValue != string.Empty)
                                properties[col].SetValue(item, xt.StringValue);
                            break;

                        default:
                            break;
                    }
                }

                Rows.Add(item);
            }

            OnUpdated(this, Rows);
        }

        protected virtual void OnUpdated(object sender, List<T> e)
        {
            Updated?.Invoke(sender, e);
        }
    }
}
