using System;
using UnityEngine;
using UnityEngine.Profiling;

namespace JFramework
{
    internal class DebugMemory
    {
        private readonly DebugData debugData;
        private long minTotalReservedMemory = 10000;
        private long maxTotalReservedMemory;
        private long minTotalAllocatedMemory = 10000;
        private long maxTotalAllocatedMemory;
        private long minTotalUnusedReservedMemory = 10000;
        private long maxTotalUnusedReservedMemory;
        private long minMonoHeapSize = 10000;
        private long maxMonoHeapSize;
        private long minMonoUsedSize = 10000;
        private long maxMonoUsedSize;
        public DebugMemory(DebugData debugData) => this.debugData = debugData;
        public void ExtendMemoryGUI()
        {
            GUILayout.BeginHorizontal();
            GUI.contentColor = Color.white;
            GUILayout.Label(debugData.GetData("Memory Information"), DebugStyle.Label,  DebugStyle.MinHeight);
            GUILayout.EndHorizontal();
            
            GUILayout.BeginVertical(DebugStyle.Box, GUILayout.Height(debugData.MaxHeight - 260));
            long memory = Profiler.GetTotalReservedMemoryLong() / 1000000;
            if (memory > maxTotalReservedMemory) maxTotalReservedMemory = memory;
            if (memory < minTotalReservedMemory) minTotalReservedMemory = memory;
            GUILayout.Label(debugData.GetData("Total Memory") + ": " + memory + "MB        " + "[" + debugData.GetData("Min") + ": " + minTotalReservedMemory + "  " + debugData.GetData("Max") + ": " + maxTotalReservedMemory + "]", DebugStyle.Label);
            memory = Profiler.GetTotalAllocatedMemoryLong() / 1000000;
            if (memory > maxTotalAllocatedMemory) maxTotalAllocatedMemory = memory;
            if (memory < minTotalAllocatedMemory) minTotalAllocatedMemory = memory;
            GUILayout.Label(debugData.GetData("Used Memory") + ": " + memory + "MB        " + "[" + debugData.GetData("Min") + ": " + minTotalAllocatedMemory + "  " + debugData.GetData("Max") + ": " + maxTotalAllocatedMemory + "]", DebugStyle.Label);
            memory = Profiler.GetTotalUnusedReservedMemoryLong() / 1000000;
            if (memory > maxTotalUnusedReservedMemory) maxTotalUnusedReservedMemory = memory;
            if (memory < minTotalUnusedReservedMemory) minTotalUnusedReservedMemory = memory;
            GUILayout.Label(debugData.GetData("Free Memory") + ": " + memory + "MB        " + "[" + debugData.GetData("Min") + ": " + minTotalUnusedReservedMemory + "  " + debugData.GetData("Max") + ": " + maxTotalUnusedReservedMemory + "]", DebugStyle.Label);
            memory = Profiler.GetMonoHeapSizeLong() / 1000000;
            if (memory > maxMonoHeapSize) maxMonoHeapSize = memory;
            if (memory < minMonoHeapSize) minMonoHeapSize = memory;
            GUILayout.Label(debugData.GetData("Total Mono Memory") + ": " + memory + "MB        " + "[" + debugData.GetData("Min") + ": " + minMonoHeapSize + "  " + debugData.GetData("Max") + ": " + maxMonoHeapSize + "]", DebugStyle.Label);
            memory = Profiler.GetMonoUsedSizeLong() / 1000000;
            if (memory > maxMonoUsedSize) maxMonoUsedSize = memory;
            if (memory < minMonoUsedSize) minMonoUsedSize = memory;
            GUILayout.Label(debugData.GetData("Used Mono Memory") + ": " + memory + "MB        " + "[" + debugData.GetData("Min") + ": " + minMonoUsedSize + "  " + debugData.GetData("Max") + ": " + maxMonoUsedSize + "]", DebugStyle.Label);
            GUILayout.EndVertical();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button(debugData.GetData("Uninstall unused resources"), DebugStyle.Button, DebugStyle.MinHeight))
            {
                Resources.UnloadUnusedAssets();
            }

            if (GUILayout.Button(debugData.GetData("Garbage Collection"), DebugStyle.Button, DebugStyle.MinHeight))
            {
                GC.Collect();
            }

            GUILayout.EndHorizontal();
        }
    }
}