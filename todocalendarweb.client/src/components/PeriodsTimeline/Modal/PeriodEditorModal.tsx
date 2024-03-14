import { CSSProperties, useEffect, useRef, useState } from 'react';
import { Modal } from '../../Modal/modal'
import './style.css'
import { useNavigate } from 'react-router-dom';
import { EditableText } from '../../EditableText/EditableText';
import TaskWrapperModes, { TaskEditor } from '../../TaskEditor/TaskEditor';
import { PeriodModel } from '../../../models/period';
import { BiTrashAlt } from 'react-icons/bi';
import usePeriods from '../../../hooks/usePeriods';

interface PeriodEditorModalProps {
    periodId: string
    onClose?: () => void
}

export const PeriodEditorModal = ({onClose= () => {}, periodId}: PeriodEditorModalProps) => {

    const [period, setPeriod] = useState<PeriodModel>()
    const periodsHook = useRef(usePeriods());
    const {deletePeriod, updatePeriod, periods} = periodsHook.current
    const navigate = useNavigate()

    useEffect(() => {
        if(periods.length != 0)
            setPeriod(periods.find(p => p.id == periodId))
    }, [])

    const close = () => {
        onClose()
        navigate('?')
    }

    const backgroundStyle: CSSProperties = {
        backgroundColor: 'rgba(110, 118, 129, 0.4)' 
    }

    const modalStyle: CSSProperties = {
        top: '0px',
        bottom: '0px',
        width: '80%',
        right: '0px',
        borderRadius: '10px 0px 0px 10px',
    }

    return(
        <Modal
            backgroundStyle={backgroundStyle}
            modalStyle={modalStyle}
            onClickOutside={close}
        >
            {
                period && 
                <div className='ModalContent'>
                    <div className="PeriodEditorHeader">
                        <EditableText
                            tag={'h2'}
                            text={period?.name}
                            onChange={name => handlePeriodUpdating(new PeriodModel(period.id, name, period.routineId, period.dayOfWeek, period.timeInterval))}
                        />
                    </div>
                    <div className="PeriodEditorBody">
                        <div className="Tasks">
                            <h3 className="TasksHeader">
                                Задачи
                            </h3>
                            <TaskEditor
                                mode={TaskWrapperModes.Editing}
                                periodId={periodId}
                            />
                        </div>
                        <div 
                            className="Deleting"
                            onClick={handlePeriodDeleting}
                        >
                            <BiTrashAlt
                            />
                            <span>
                                Удалить период
                            </span>
                        </div>
                    </div>
                </div>
            }
        </Modal>
    )

    function handlePeriodDeleting() {
        deletePeriod(periodId)
        close()
    }

    async function handlePeriodUpdating(changedPeriod: PeriodModel){
        updatePeriod(changedPeriod)
    }
    
}