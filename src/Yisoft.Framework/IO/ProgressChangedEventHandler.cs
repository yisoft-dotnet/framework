// ===============================================================================
// Website: https://yi.team/
// Copyright © Yi.TEAM. All rights reserved.
// ===============================================================================

namespace Yisoft.Framework.IO
{
    /// <summary>
    /// 表示将处理 <see cref="ProgressStream"/> 类的 <see cref="ProgressStream.ProgressChanged"/> 事件的方法。 此类不能被继承。
    /// </summary>
    /// <param name="sender">事件源。</param>
    /// <param name="e">包含事件数据的 <see cref="ProgressChangedEventArgs"/>。</param>
    public delegate void ProgressChangedEventHandler(object sender, ProgressChangedEventArgs e);
}