import {ReactNode, useState } from "react"
import './style.css'
import { BiMinus, BiPlus } from "react-icons/bi"


interface SwiperProps{
    children: ReactNode[]
    slidesPerView: number
}

export const Swiper = ({children, slidesPerView}: SwiperProps) => {

    const [startSlide, setStartSlide] = useState(0);

    return (
        <div className="Swiper">
            <BiMinus onClick={() => moveTo(-1)}/>
            {
                children.map((child, index) =>
                {
                    if(index >= startSlide && index < slidesPerView+startSlide){
                        return  <div key={index} className="Slide">
                                    {child}
                                </div>
                    }
                }
                )
            }
            <BiPlus onClick={() => moveTo(1)}/>
        </div>
    )

    function moveTo(direction: number){
        if(startSlide + direction >= 0 && startSlide + direction <= children.length - slidesPerView)
            setStartSlide(startSlide + direction)
    }
}