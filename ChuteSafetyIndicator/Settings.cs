// 
//     Chute Safety Indicator
// 
//     Copyright (C) 2015 Sean McDougall
// 
//     This program is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
// 
//     You should have received a copy of the GNU General Public License
//     along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using KSPPluginFramework;

namespace ChuteSafetyIndicator
{
    public class Settings : ConfigNodeStorage
    {
        internal Settings(String FilePath) : base(FilePath) { }

        [Persistent]
        internal Color safeColor = new Color(0f, 0.7f, 0f);

        [Persistent]
        internal Color riskyColor = new Color(1f, 1f, 0f);

        [Persistent]
        internal Color unSafeColor = new Color(0.9f, 0f, 0f);

    }
}
