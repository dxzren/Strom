using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;

public class BP_DragView : UIDragDropItem
{
    private GameObject          SourceParent                = null;
    private string              SourceTag                   = "";
    public void ViewInit()
    {

    }
    protected override void OnDragDropStart()
    {
        Debug.Log("OnDragDropStar");
        SourceParent            = this.transform.parent.gameObject;
    }
    protected override void OnDragDropMove(Vector3 delta)
    {
        mTrans.localPosition += delta;
    }
    protected override void OnDragDropRelease(GameObject surface)
    {
        Debug.Log("OnDragDropRelease");
        UIDragDropContainer Container = surface ? NGUITools.FindInParents<UIDragDropContainer>(surface) : null;

        if( Container != null )
        {
            Debug.Log("Container != null");
            if      (surface.tag == "Item" && surface.transform.parent.childCount == 2)
            {                                                                                                                               
                this.transform.SetParent(surface.transform.parent.transform);                   // 当前Item父级   => 目标父级     2:2 => 1:3
                SourceParent.transform.GetChild(0).SetParent(this.transform.parent.transform);  // 原始子集(Hero) => 当前父级     1:3 => 0:4

                surface.transform.parent.GetChild(1).SetParent(SourceParent.transform);         // 目标子集(hero) => 原始父级     0:4 => 1:3
                surface.transform.SetParent(SourceParent.transform);                            // 目标子集(Item) => 原始父级     1:3 => 2:2

                this.transform.localPosition                                = Vector3.zero;
            }
            else if (surface.tag == "Item" && surface.transform.parent.childCount == 1)
            {
                this.transform.SetParent(surface.transform.parent.transform);                   // 当前Item父级   => 目标父级     2:1 => 1:2
                surface.transform.SetParent(SourceParent.transform);                            // 目标子集(Item) => 原始父级     1:3 => 2:2


                surface.transform.localPosition                             = Vector3.zero;
            }
        }

        if (!cloneOnDrag)
        {
            mTouchID = int.MinValue;

            // Re-enable the collider
            if (mButton != null) mButton.isEnabled = true;
            else if (mCollider != null) mCollider.enabled = true;
            else if (mCollider2D != null) mCollider2D.enabled = true;

            // Is there a droppable container?
            UIDragDropContainer container = surface ? NGUITools.FindInParents<UIDragDropContainer>(surface) : null;

            if (container != null)
            {
                // Container found -- parent this object to the container
                mTrans.parent = (container.reparentTarget != null) ? container.reparentTarget : container.transform;

                Vector3 pos = mTrans.localPosition;
                pos.z = 0f;
                mTrans.localPosition = pos;
            }
            else
            {
                // No valid container under the mouse -- revert the item's parent
                mTrans.parent = mParent;
            }

            // Update the grid and table references
            mParent = mTrans.parent;
            mGrid = NGUITools.FindInParents<UIGrid>(mParent);
            mTable = NGUITools.FindInParents<UITable>(mParent);

            // Re-enable the drag scroll view script
            if (mDragScrollView != null)
                StartCoroutine(EnableDragScrollView());

            // Notify the widgets that the parent has changed
            NGUITools.MarkParentAsChanged(gameObject);

            if (mTable != null) mTable.repositionNow = true;
            if (mGrid != null) mGrid.repositionNow = true;
        }
        else NGUITools.Destroy(gameObject);
    }
}
