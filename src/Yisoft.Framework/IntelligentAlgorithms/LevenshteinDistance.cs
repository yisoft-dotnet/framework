//      )                             *     
//   ( /(        *   )       (      (  `    
//   )\()) (   ` )  /( (     )\     )\))(   
//  ((_)\  )\   ( )(_)))\ ((((_)(  ((_)()\  
// __ ((_)((_) (_(_())((_) )\ _ )\ (_()((_) 
// \ \ / / (_) |_   _|| __|(_)_\(_)|  \/  | 
//  \ V /  | | _ | |  | _|  / _ \  | |\/| | 
//   |_|   |_|(_)|_|  |___|/_/ \_\ |_|  |_| 
// 
// This file is subject to the terms and conditions defined in
// file 'License.txt', which is part of this source code package.
// 
// Copyright © Yi.TEAM. All rights reserved.
// -------------------------------------------------------------------------------

using System;

namespace Yisoft.Framework.IntelligentAlgorithms
{
	/// <summary>
	/// 使用编辑距离算法(Levenshtein Distance)实现字符串相似度的运算。
	/// </summary>
	public static class LevenshteinDistance
	{
		/// <summary>
		/// 计算编辑距离。
		/// </summary>
		/// <param name="str1">参与运算的第一个字符串。</param>
		/// <param name="str2">参与运算的第二个字符串。</param>
		/// <returns>返回 <see cref="LevenshteinDistanceResult"/>。</returns>
		public static LevenshteinDistanceResult Compute(string str1, string str2)
		{
			if (str1 == null) throw new ArgumentNullException(nameof(str1));
			if (str2 == null) throw new ArgumentNullException(nameof(str2));

			var computeTimes = 0;
			var arrChar1 = str1.ToCharArray();
			var arrChar2 = str2.ToCharArray();
			var row = arrChar1.Length + 1;
			var column = arrChar2.Length + 1;
			var matrix = new int[row, column];

			for (var i = 0; i < column; i++) matrix[0, i] = i;
			for (var i = 0; i < row; i++) matrix[i, 0] = i;

			for (var i = 1; i < row; i++)
			{
				for (var j = 1; j < column; j++)
				{
					var intCost = arrChar1[i - 1] == arrChar2[j - 1] ? 0 : 1;

					matrix[i, j] = _Minimum(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1, matrix[i - 1, j - 1] + intCost);
					computeTimes++;
				}
			}

			var intLength = row > column ? row : column;
			var distance = matrix[row - 1, column - 1];
			var rate = 1 - Convert.ToDouble(distance) / intLength;

			return new LevenshteinDistanceResult(str1, str2, computeTimes, distance, rate);
		}

		private static int _Minimum(int first, int second, int third)
		{
			var intMin = first;

			if (second < intMin) intMin = second;
			if (third < intMin) intMin = third;

			return intMin;
		}
	}
}
