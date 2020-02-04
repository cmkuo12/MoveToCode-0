﻿using UnityEngine;

namespace MoveToCode {
    public class ExitInstructionSnapCollider : InstructionSnapCollider {
        public override void DoSnapAction(CodeBlock myCodeBlock, CodeBlock collidedCodeBlock) {
            Vector3 relationToParent = transform.localPosition + collidedCodeBlock.GetSnapToParentPosition();
            relationToParent.y = -1f;
            (myCodeBlock as ControlFlowCodeBlock).SetExitCodeBlock(collidedCodeBlock, relationToParent);
        }
    }
}