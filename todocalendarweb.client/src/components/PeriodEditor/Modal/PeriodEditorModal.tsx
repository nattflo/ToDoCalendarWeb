import { CSSProperties, useEffect, useState } from 'react';
import { Modal } from '../../Modal/modal'
import './style.css'
import { useNavigate } from 'react-router-dom';
import { EditableText } from '../../EditableText/EditableText';
import TaskWrapperModes, { TaskEditor } from '../../TaskEditor/TaskEditor';
import { Period, PeriodSchema } from '../../../models/period';
import { httpDelete, httpGet, httpPut } from '../../../utils/http';
import { BiTrashAlt } from 'react-icons/bi';

interface PeriodEditorModalProps {
    periodId: string
    onClose?: () => void
}

export const PeriodEditorModal = ({onClose= () => {}, periodId}: PeriodEditorModalProps) => {

    const [period, setPeriod] = useState<Period>()
    const [isLoading, setIsLoading] = useState(true)
    const navigate = useNavigate();

    useEffect(() => {
        fetchPeriod();
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
                !isLoading && period != undefined &&
                <div className='ModalContent'>
                    <div className="PeriodEditorHeader">
                        <EditableText
                            tag={'h2'}
                            text={period?.name}
                            onChange={name => updatePeriod(new Period(period.id, name, period.routineId, period.dayOfWeek, period.timeInterval))}
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
                            onClick={() => deletePeriod()}
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

    function deletePeriod() {
        httpDelete('periods', periodId)
        close()
    }

    async function fetchPeriod() {
        setIsLoading(true)
        const periodSchema = await httpGet<PeriodSchema>('periods/'+ periodId)
        const period = Period.createFromSchema(periodSchema)
        setPeriod(period)
        setIsLoading(false)
    }

    async function updatePeriod(changedPeriod: Period){
        httpPut<PeriodSchema>('periods', changedPeriod.id, changedPeriod.schema)
        setPeriod(period)
    }
}