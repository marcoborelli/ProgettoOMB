using System;

namespace HydrogenOMB {
    public interface IDataManager {
        void OnStart();
        void OnEndOpen();
        void OnStop();
        void OnForcedStop();
        void OnEndArrayOpen();
        void OnEndArrayClose();
        void OnData(string row, DateTime oldTime);
    }
}
