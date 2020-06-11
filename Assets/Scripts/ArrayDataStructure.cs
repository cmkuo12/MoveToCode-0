﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace MoveToCode {
    public class ArrayDataStructure : IDataType {
        int size;
        IDataType[] internalArray;
        Type arrayType;

        public ArrayDataStructure(CodeBlock cbIn) : base(cbIn) { }
        public ArrayDataStructure(CodeBlock cbIn, int size) : base(cbIn) {
            //SetValue(valIn);
            SetSize(size);
            arrayType = typeof(int); //default
        }

        //TODO: Fix this
        public override bool IsSameDataTypeAndEqualTo(IDataType otherVal) {
            if (otherVal is ArrayDataStructure) {
                return (string)GetValue() == (string)otherVal.GetValue();
            }
            throw new InvalidOperationException("Trying to compare Array to non Array");
        }

        public override void SetValue(object valIn) {
            throw new InvalidOperationException("Trying to set a value in the array without specifying an index");
        }

        public override object GetValue() {
            return 0;
            //throw new InvalidOperationException("Trying to get a value from the array without specifying an index");
        }

        public override int GetNumArguments() {
            return internalArray.Length;
        }

        //set length of array, should only be called in constructor
        private void SetSize(int sizeIn) {
            size = sizeIn;
            internalArray = new IDataType[size];
        }

        //get size of array (not the number of elements of the array)
        public int GetSize() {
            return size;
        }

        public bool Empty() {
            for(int i = 0; i < size; i++) {
                if(internalArray[i] != null) {
                    return false;
                }
            }
            return true;
        }

        public void SetValueAtIndex(int index) {
            //first input sets the type of array
            if(Empty()) {
                internalArray[index] = GetArgumentAt(index).EvaluateArgument();
                arrayType = internalArray[index].GetType();
                SetUpArgPosToCompatability();
            }
            if(index < size) {
                internalArray[index] = GetArgumentAt(index).EvaluateArgument();
            } else {
                throw new InvalidOperationException("Trying to read beyond array length");
            }
        }

        public override void EvaluateArgumentList() {
            for(int i = 0; i < size; i++) {
                internalArray[i] = GetArgumentAt(i)?.EvaluateArgument().GetValue() as IDataType;
            }
        }

        //TODO: maybe operator overload instead?
        public IDataType GetValueAtIndex(int index) {
            return internalArray[index];
        }

        public override void SetUpArgPosToCompatability() {
            argPosToCompatability = new List<List<Type>> { };
            for (int i = 0; i < GetSize(); i++) {
                argPosToCompatability.Add(new List<Type> {
                    typeof(IDataType)
                });
            }
        }

        //TODO: Fix this
        public override void SetUpArgDescriptionList() {
            argDescriptionList = new List<string> { "Left side of condtional", "Right Side of Conditional" };
        }

        //TODO: Fix this
        public override Type GetCastType() {
            return typeof(int);
        }
    }
}

