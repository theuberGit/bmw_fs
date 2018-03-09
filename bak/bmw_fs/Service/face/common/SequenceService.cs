using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bmw_fs.Service.face.common
{
    interface SequenceService
    {
        int getSequence(String id);

        int getSequenceMasterIdx();
    }
}
