//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright 漏 2011-2014 Tasharen Entertainment
//----------------------------------------------
//_______________________________________________________________________________________________
//
//  OnDragStart()                                         在发送OnDrag()通知之前触发
//  OnDrag                    (delta)                     发送到正在被拖动的对象
//  OnDragOver                (draggedObject)             发送给一个对象，当另一个对象被拖到它的区域时
//  OnDragOut                 (draggedObject)             发送给一个对象，当另一个对象被拖出它的区域时
//  OnDrop                    (draggedObject)             接收拖拽的对象信息
//  OnDragEnd()                                           当拖动事件结束时被发送到一个dragged对象
//________________________________________________________________________________________________
using UnityEngine;
using System.Collections;

/// <summary>
/// 拖放操作基础脚本
/// </summary>

[AddComponentMenu("NGUI/Interaction/Drag and Drop Item")]
public class UIDragDropItem : MonoBehaviour
{
	public enum                 Restriction                                                         // 限制条件 
	{
		None,
		Horizontal,                                                                                 // 水平
		Vertical,                                                                                   // 垂直
		PressAndHold,                                                                               // 按住
	}                                                                      

	public Restriction          restriction                     = Restriction.None;                 // 拖放逻辑限制。
    public bool                 cloneOnDrag                     = false;                            // 是否拖放项目为副本

	[HideInInspector]
	public float                pressAndHoldDelay               = 1f;                               // 按住多少时间_开始拖动

    #region=============================================||    通用方法    ||=================================================        

    protected int               mTouchID                        = int.MinValue;                     // 
    protected float             mPressTime                      = 0f;                               // 

    protected Transform         mTrans;                                                             /// 本体坐标
	protected Transform         mParent;                                                            /// 父级坐标
	protected Collider          mCollider;                                                          /// 碰撞变量
	protected Collider2D        mCollider2D;                                                        /// 2D 碰撞
	protected UIButton          mButton;                                                            /// UI Btn
	protected UIRoot            mRoot;                                                              /// Root
	protected UIGrid            mGrid;                                                              /// 格子排列 
	protected UITable           mTable;                                                             /// 表
	protected UIDragScrollView  mDragScrollView                 = null;                             /// 滚动视图          
    protected virtual void Start ()                                                                 // 缓存转换 
	{
		mTrans                  = transform;                                                        // 坐标转换
		mCollider               = GetComponent<Collider>();                                         // 碰撞转换
		mCollider2D             = GetComponent<Collider2D>();                                       // 2D碰撞转换
        mButton                 = GetComponent<UIButton>();                                         // 按钮转换
		mDragScrollView         = GetComponent<UIDragScrollView>();                                 // 滚动视图转换
	}
	void OnPress                (bool isPressed) { if (isPressed) mPressTime = RealTime.time; }     /// 记录项目被按下的时间。

    void                        OnDragStart ()                                                                      // 启动拖动参数设置 
    {
		if (!enabled || mTouchID != int.MinValue)               return;

		if (restriction != Restriction.None)                                                        /// 检测拖动限制条件    
		{
			if (restriction == Restriction.Horizontal)
			{
				Vector2 delta = UICamera.currentTouch.totalDelta;
				if (Mathf.Abs(delta.x) < Mathf.Abs(delta.y)) return;
			}
			else if (restriction == Restriction.Vertical)
			{
				Vector2 delta = UICamera.currentTouch.totalDelta;
				if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y)) return;
			}
			else if (restriction == Restriction.PressAndHold)
			{
				if (mPressTime + pressAndHoldDelay > RealTime.time) return;
			}
		}

		if (cloneOnDrag)                                                                            /// 使用拖动副本 设置参数      
		{
			GameObject          clone = NGUITools.AddChild(transform.parent.gameObject, gameObject);                // 在父级 添加副本子对象
			clone.transform.localPosition                       = transform.localPosition;                          // 位移坐标.
			clone.transform.localRotation                       = transform.localRotation;                          // 旋转参数.
			clone.transform.localScale                          = transform.localScale;                             // 缩放参数.

            UIButtonColor       bc                              = clone.GetComponent<UIButtonColor>();              // 获取按钮状态颜色
			if (bc != null)     bc.defaultColor                 = GetComponent<UIButtonColor>().defaultColor;       // 初始化颜色

			UICamera.currentTouch.dragged                       = clone;                                            // 摄影机目标 为副本

			UIDragDropItem      item                            = clone.GetComponent<UIDragDropItem>();             // 副本获取<Drag>为 Item 
			item.Start();                                                                                           // 副本启动
			item.OnDragDropStart();                                                                                 // 副本拖动启动初始化
		}
		else OnDragDropStart();                                                                     /// 执行与启动拖放操作相关的任何逻辑。
	}
    void                        OnDrag (Vector2 delta)                                                              // 执行拖动        
	{
		if (!enabled || mTouchID != UICamera.currentTouchID) return;                                
		OnDragDropMove          ((Vector3)delta * mRoot.pixelSizeAdjustment);                                       /// 调整拖动对象的位置
    }
	void                        OnDragEnd ()                                                                        // 拖动结束        
	{
		if (!enabled || mTouchID != UICamera.currentTouchID) return;
		OnDragDropRelease(UICamera.hoveredObject);
	}

#endregion
	protected virtual void      OnDragDropStart ()                                                                  // 执行与启动拖放操作相关的任何逻辑。
    {
        if      ( mDragScrollView != null)       mDragScrollView.enabled            = false;                        /// 自动禁用滚动视图                    
        if      ( mButton != null)               mButton.isEnabled                  = false;                        /// 关闭按钮        防止拦截事件
		else if ( mCollider != null)             mCollider.enabled                  = false;                        /// 关闭碰撞        防止拦截事件
		else if ( mCollider2D != null)           mCollider2D.enabled                = false;                        /// 关闭2D碰撞      防止拦截事件

        mTouchID                = UICamera.currentTouchID;                                 
		mParent                 = mTrans.parent;
		mRoot                   = NGUITools.FindInParents<UIRoot>(mParent);
		mGrid                   = NGUITools.FindInParents<UIGrid>(mParent);
		mTable                  = NGUITools.FindInParents<UITable>(mParent);

		if (UIDragDropRoot.root != null)        mTrans.parent = UIDragDropRoot.root;                                /// 重设项父级                          


        Vector3 pos             = mTrans.localPosition;
		pos.z                   = 0f;
		mTrans.localPosition    = pos;

		TweenPosition           tp              = GetComponent<TweenPosition>();                                    /// 关闭 坐标位移
		if (tp != null)         tp.enabled      = false;

		SpringPosition          sp              = GetComponent<SpringPosition>();                                   /// 关闭 设置回弹力
		if (sp != null)         sp.enabled      = false;
		NGUITools.MarkParentAsChanged(gameObject);                                                                  /// 通知小部件父项已更改

        if (mTable != null)     mTable.repositionNow    = true;                                                     /// Table下次更新 重新定位子项
		if (mGrid != null)      mGrid.repositionNow     = true;                                                     /// Grid 下次更新 重新定位子项
	}

    protected virtual void      OnDragDropMove (Vector3 delta)                                                      // 调整拖动对象的位置     
	{
		mTrans.localPosition += delta;
	}

	protected virtual void      OnDragDropRelease (GameObject surface)                                              // 项目拖动到指定对象上   
	{
		if (!cloneOnDrag)
		{
			mTouchID = int.MinValue;

			// Re-enable the collider
			if      (mButton != null)           mButton.isEnabled = true;
			else if (mCollider != null)          mCollider.enabled = true;
			else if (mCollider2D != null)        mCollider2D.enabled = true;

			// Is there a droppable container?
			UIDragDropContainer container = surface ? NGUITools.FindInParents<UIDragDropContainer>(surface) : null;         //

			if (container != null)                                                      // 
			{
                // Container found -- parent this object to the container               
                mTrans.parent = (container.reparentTarget != null) ? container.reparentTarget : container.transform;

				Vector3 pos = mTrans.localPosition;
				pos.z = 0f;
				mTrans.localPosition = pos;
			}
			else
			{
                // No valid container under the mouse -- revert the item's parent       ( 鼠标下没有有效的容器--还原项的父项 )
                mTrans.parent = mParent;
			}

            // Update the grid and table references                                     ( 更新Grid和 表引用 )
            mParent = mTrans.parent;
			mGrid = NGUITools.FindInParents<UIGrid>(mParent);
			mTable = NGUITools.FindInParents<UITable>(mParent);

            if (mDragScrollView != null)                                                // 重新启用拖动滚动视图脚本)
                StartCoroutine(EnableDragScrollView());

            NGUITools.MarkParentAsChanged(gameObject);                                  // 通知小部件父项已更改 

            if (mTable != null) mTable.repositionNow = true;                            // 更新表
			if (mGrid != null) mGrid.repositionNow = true;                              // 更新格子
		}   
		else NGUITools.Destroy(gameObject);
	}

	/// <summary>
	/// Re-enable the drag scroll view script at the end of the frame.
	/// Reason: http://www.tasharen.com/forum/index.php?topic=10203.0
	/// </summary>

	protected IEnumerator       EnableDragScrollView ()
	{
		yield return new        WaitForEndOfFrame();
		if (mDragScrollView != null) mDragScrollView.enabled = true;
	}
}
